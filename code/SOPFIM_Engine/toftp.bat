@echo off
echo open ftp.esricanada.com>ftp.txt
echo queadmin>>ftp.txt
echo gaspe>>ftp.txt
echo cd outgoing>>ftp.txt
echo cd SOPFIM>>ftp.txt
echo mkdir setup_%date:~6,4%_%date:~3,2%_%date:~0,2%>>ftp.txt
echo cd setup_%date:~6,4%_%date:~3,2%_%date:~0,2%>>ftp.txt
echo binary>>ftp.txt
echo put SopfimMessageSetup/Debug/Sopfim_Suivi_Message_Setup.msi>>ftp.txt
echo put SopfimMessageSetup/Debug/setup.exe>>ftp.txt
echo disconnect>>ftp.txt
echo bye>>ftp.txt
ftp -i -s:ftp.txt