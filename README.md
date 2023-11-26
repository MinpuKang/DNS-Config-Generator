## DNS-Config-Generator

#### Function

This is a free software for generating DNS config of GSM/WCDMA/EPS/5GS mobility/session management and Wi-Fi Service based on 3GPP 29.303 for DNS and 23.003 for Numbering, addressing and identification.

If no IP filled, then after submit, FQDN for dig/nslookup CLIs is generated with which service supports by this FQDN

Fill info based on the mapping between ID and IP according 3GPP 29.303, then submit, it will generate DNS configuration and then save input to excel as design template.

A format excel as input can be loaded to fill all requirement automatically.

How to get the template of input? all items can be filled then then submit to get a template.

#### How to get the EXE

It is already built in [bin/release](https://github.com/MinpuKang/DNS-Config-Generator/blob/master/DNS%20Config%20Public/bin/Release/DNS%20Config%20Public.exe)


#### Merge manifest

If this repository is forked or downloaded, and built locally, to run mt.exe for merging manifest into exe is needed to build one exe file.

Below one is for Windows 10:
```bash
"C:\Program Files (x86)\Windows Kits\10\bin\10.0.22621.0\x86\mt.exe" -manifest "..\bin\Release\DNS Config Public.exe.manifest" -outputresource:"..\bin\Release\DNS Config Public.exe";1
```