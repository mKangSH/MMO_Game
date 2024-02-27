
protoc.exe -I=./ --csharp_out=./ ./Protocol.proto

IF ERRORLEVEL 1 PAUSE

START ../../Server/PacketGenerator/bin/Release/PacketGenerator.exe ./Protocol.proto
XCOPY /Y Protocol.cs "../../Client/Assets/Scripts/Packet"
XCOPY /Y Protocol.cs "../../Server/CSharpServer/Packet"
XCOPY /Y ClientPacketManager.cs "../../Client/Assets/Scripts/Packet"
XCOPY /Y ServerPacketManager.cs "../../Server/CSharpServer/Packet"