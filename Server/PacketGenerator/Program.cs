﻿using System;
using System.Xml;

namespace PacketGenerator
{
    class Program
    {
        static string _genPackets;
        static ushort _packetId;
        static string _packetEnums;

        static string _clientManagerRegister;
        static string _serverManagerRegister;

        static void Main(string[] args)
        {
            string pdlPath = "../../PDL.xml";

            XmlReaderSettings settings = new XmlReaderSettings()
            {
                IgnoreComments = true,
                IgnoreWhitespace = true,
            };

            if(args.Length > 0) 
            {
                pdlPath = args[0];
            }

            using (XmlReader r = XmlReader.Create(pdlPath, settings))
            {
                r.MoveToContent();

                while(r.Read())
                {
                    if(r.Depth == 1 && r.NodeType == XmlNodeType.Element)
                    {
                        ParsePacket(r);
                    }
                }

                string fileText = string.Format(PacketFormat.fileFormat, _packetEnums, _genPackets);
                File.WriteAllText("GenPackets.cs", fileText);

                string clientManagerText = string.Format(PacketFormat.managerFormat, _clientManagerRegister);
                File.WriteAllText("ClientPacketManager.cs", clientManagerText);

                string serverManagerText = string.Format(PacketFormat.managerFormat, _serverManagerRegister);
                File.WriteAllText("ServerPacketManager.cs", serverManagerText);
            }
        }

        public static void ParsePacket(XmlReader r)
        {
            if(r.NodeType == XmlNodeType.EndElement)
            {
                return;
            }

            if(r.Name.ToLower() != "packet")
            {
                Console.WriteLine("Invalid packet node.");
                return;
            }

            string packetName = r["name"];
            if(string.IsNullOrEmpty(packetName))
            {
                Console.WriteLine("Packet Name is Empty.");
                return;
            }

            Tuple<string, string, string> t = ParseMembers(r);
            _genPackets += string.Format(PacketFormat.packetFormat, packetName, t.Item1, t.Item2, t.Item3);

            if(string.IsNullOrEmpty(_packetEnums) == false)
            {
                _packetEnums += $"{Environment.NewLine}\t";
            }
            _packetEnums += string.Format(PacketFormat.packetEnumFormat, packetName, ++_packetId);

            if(packetName.StartsWith("S_") || packetName.StartsWith("s_"))
            {
                if (string.IsNullOrEmpty(_clientManagerRegister) == false)
                {
                    _clientManagerRegister += $"{Environment.NewLine}{Environment.NewLine}\t\t";
                }
                _clientManagerRegister += string.Format(PacketFormat.managerRegisterFormat, packetName);
            }

            else
            {
                if (string.IsNullOrEmpty(_serverManagerRegister) == false)
                {
                    _serverManagerRegister += $"{Environment.NewLine}{Environment.NewLine}\t\t";
                }
                _serverManagerRegister += string.Format(PacketFormat.managerRegisterFormat, packetName);
            }
        }

        // {1} : 멤버 변수
        // {2} : 멤버 변수 Read
        // {3} : 멤버 변수 Write
        public static Tuple<string, string, string> ParseMembers(XmlReader r)
        {
            string packetName = r["name"];

            string memberCode = "";
            string readCode = "";
            string writeCode = "";

            int depth = r.Depth + 1;
            while(r.Read())
            {
                if(r.Depth != depth)
                {
                    break;
                }

                string memberName = r["name"];
                if (string.IsNullOrEmpty(memberName))
                {
                    Console.WriteLine("Member Name is Empty.");
                    return null;
                }

                if(string.IsNullOrEmpty(memberCode) == false)
                {
                    memberCode += Environment.NewLine;
                }

                if (string.IsNullOrEmpty(readCode) == false)
                {
                    readCode += Environment.NewLine;
                }

                if (string.IsNullOrEmpty(writeCode) == false)
                {
                    writeCode += Environment.NewLine;
                }

                string memberType = r.Name.ToLower();
                switch(memberType)
                {
                    case "byte":
                    case "sbyte":
                        memberCode += string.Format(PacketFormat.memberFormat, memberType, memberName);
                        readCode += string.Format(PacketFormat.readByteFormat, memberName, memberType);
                        writeCode += string.Format(PacketFormat.writeByteFormat, memberName, memberType);
                        break;
                    case "bool":
                    case "short":
                    case "ushort":
                    case "int":
                    case "long":
                    case "float":
                    case "double":
                        memberCode += string.Format(PacketFormat.memberFormat, memberType, memberName);
                        readCode += string.Format(PacketFormat.readFormat, memberName, ToMemberType(memberType), memberType);
                        writeCode += string.Format(PacketFormat.writeFormat, memberName, memberType);
                        break;
                    case "string":
                        memberCode += string.Format(PacketFormat.memberFormat, memberType, memberName);
                        readCode += string.Format(PacketFormat.readStringFormat, memberName);
                        writeCode += string.Format(PacketFormat.writeStringFormat, memberName);
                        break;
                    case "list":
                        Tuple<string, string, string> t = ParseList(r);
                        memberCode += t.Item1;
                        readCode += t.Item2;
                        writeCode += t.Item3;
                        break;
                    default:
                        break;
                }
            }

            memberCode = memberCode.Replace("\n", "\n\t");
            readCode = readCode.Replace("\n", "\n\t\t");
            writeCode = writeCode.Replace("\n", "\n\t\t");
            return new Tuple<string, string, string>(memberCode, readCode, writeCode);
        }

        public static Tuple<string, string, string> ParseList(XmlReader r)
        {
            string listName = r["name"];
            if(string.IsNullOrEmpty(listName))
            {
                Console.WriteLine("List Name is Empty");
                return null;
            }

            Tuple<string, string, string> t = ParseMembers(r);

            string memberCode = string.Format(PacketFormat.memberListFormat,
                FirstCharToUpper(listName), 
                FirstCharToLower(listName), 
                t.Item1, 
                t.Item2, 
                t.Item3);

            string readCode = string.Format(PacketFormat.readListFormat, 
                FirstCharToUpper(listName), 
                FirstCharToLower(listName));

            string writeCode = string.Format(PacketFormat.writeListFormat,
                FirstCharToUpper(listName), 
                FirstCharToLower(listName));

            return new Tuple<string, string, string>(memberCode, readCode, writeCode);
        }

        public static string ToMemberType(string memberType)
        {
            switch (memberType)
            {
                case "bool":
                    return "ToBoolean";
                case "short":
                    return "ToInt16";
                case "ushort":
                    return "ToUInt16";
                case "int":
                    return "ToInt32";
                case "long":
                    return "ToInt64";
                case "float":
                    return "ToSingle";
                case "double":
                    return "ToDouble";
                default:
                    return "";
            }
        }

        public static string FirstCharToUpper(string input)
        {
            if(string.IsNullOrEmpty(input))
            {
                return "";
            }

            return input[0].ToString().ToUpper() + input.Substring(1);
        }

        public static string FirstCharToLower(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            return input[0].ToString().ToLower() + input.Substring(1);
        }
    }
}

