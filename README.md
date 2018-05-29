# Purpose 
A Repository manager is a library that can be used to store and retrieve JSON string or XML string. A unique string is used to indicate the item being stored.

# Prerequisite
Download and Install GIT Client https://git-scm.com/downloads
Download and install .NET Core SDK

Mac OS

https://www.microsoft.com/net/download/thank-you/dotnet-sdk-2.1.200-macos-x64-installer

Windows

https://www.microsoft.com/net/download/thank-you/dotnet-sdk-2.1.200-windows-x64-installer

# How to Run Test with xUnit ( dotnet CLI )

Open your terminal on your Mac or Command Prompt Windows;

> git clone https://github.com/tru3d3v/netcore_repository_manager_with_xUnit.git

> cd netcore_repository_manager_with_xUnit

> dotnet build Formulatrix.sln 

> cd xUnitTest

> dotnet test


# Screenshoot xUnitTest ( dotnet CLI )
![alt text](https://cdn.pbrd.co/images/HnlP6Xk.png)


# List Of xUnitTest

[Theory]

[InlineData("item1","{\"to\":\"tru3.d3v@gmail.com\",\"title\":\"Greetings\",\"body\":\"Hi there!\"}",1)]

[InlineData("item2", "<to>tru3</to><title>Greetings</title><body>Hithere</body>", 2)]

ReturnTrue_GivenParameters_RegisteringJSONXML

[Fact]

ReturnFalse_GivenDuplicatedItemNameAndItemType_RegisteringJSON

ReturnFalse_GivenDuplicatedItemNameAndItemType_RegisteringXML

ReturnFalse_GivenNoInputParameters_Registering

Return1_GivenitemName_getType

Return2_GivenitemName_getType

Return0_GivenitemNameWrong_getType

ReturnStringXML_GivenitemName_Retrieve

ReturnStringJson_GivenitemName_Retrieve

ReturnStringEmpty_GivenitemNameWrong_Retrieve



