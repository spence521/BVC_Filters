#set -o nounset
mcs Classes/*.cs
mv Classes/BloomFilter.exe Classes/BVC_Filters.exe
mono Classes/BVC_Filters.exe
#rm Classes/BVC_Filters.exe
exit 0