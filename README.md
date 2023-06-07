# PDGAClubMemberRatings
Console app that uses the Disc Golf Scene registration list CSV file to look up player ratings using the PDGA's API. 

This app requires you to have a token for the API.
https://www.pdga.com/dev

The app expects 4 parameters:
1. Input File Name (The CSV from Disc Golf Scene)
2. Output File Name 
3. PDGA User Name (FirstName LastName PDGA#)
4. PDGA Password

Sample Use:

PdgaClubMembers.exe input.csv output.csv "First Last 12345" password