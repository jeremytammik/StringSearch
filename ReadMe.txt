==================================================
       Plugin of the Month, October 2011
 Brought to you by the Autodesk Developer Network
==================================================

---------------
 String Search
---------------


Description
-----------

Search Revit project elements and their parameters for a given string.


System Requirements
-------------------

This plugin has been tested with Revit Architecture 2012 and 
requires the .NET Framework 3.5.

A pre-built version of the plugin has been provided which should
work on 32- and 64-bit Windows systems.

The plugin has not been tested with all Revit verticals, 
but should work (see "Feedback", below, otherwise).

The source code has been provided as a Visual Studio 2010 project
containing C# code (not required to run the plugin).


Installation
------------

1. Copy the plugin assembly module "ADNPlugin-StringSearch.dll" 
to a location on your local system (for example, your Revit-based 
application's root program folder).

2. Open the addin manifest file "ADNPlugin-StringSearch.addin" 
in Notepad. Modify the following line to match to the location of
the plugin module that you have just copied, if it is different,
and change the Revit version if required:

<Assembly>C:\Program Files\Autodesk\Revit Architecture 2012\
Program\ADNPlugin-StringSearch.dll</Assembly>
 
Save and close the file. 

3. Copy the addin manifest file you just modified to one of the 
following locations: 

For Windows XP: 

C:\Documents and Settings\All Users\Application Data\
Autodesk\Revit\Addins\2012\

or 

C:\Documents and Settings\<your login>\Application Data\
Autodesk\Revit\Addins\2012\

(The first location will make the plugin available to all users of
your computer, while the second is for your use only.)

For Vista/Windows 7:

C:\ProgramData\Autodesk\Revit\Addins\2012\

or 

C:\Users\<your login>\AppData\Roaming\Autodesk\Revit\Addins\2012\

(The first location will make the plugin available to all users 
of your computer, while the second is for your use only.) 

4. Once installed, the String Search command becomes available 
in your Revit.  Go to the "Add-Ins" tab > "String Search" panel. 
It contains a "String Search" button to launch the command.


Usage
-----

Inside your Revit-based application, click "Add-Ins" > "String Search" 
> "String Search" to launch the command. 

In the dialogue that appears, you can type in the search string. 
Click 'OK'. The command will search for the given string on all
standard parameters of all elements in the current view.

The elements and parameters to serch can be modified.

All occurences of the search string are listed in a data grid view 
in a modeless navigator dialogue. Double click on a row in the navigator 
to zoom to the element.


Uninstallation
--------------

Simply removing "ADNPlugin-StringSearch.addin" file from 
your installation folder will uninstall the plugin. 

Limitations
--------------


Known Issues
------------


Author
------

This plugin was written by Jeremy Tammik of the 
Autodesk Developer 
Technical Services team. 

Acknowledgements
----------------


Further Reading
---------------

For more information on developing with Revit, please visit the
Revit Developer Center at http://www.autodesk.com/developrevit.


Feedback
--------

Email us at labs.plugins@autodesk.com with feedback 
or requests for 
enhancements.


Release History
---------------

2011-10-01 2012.0.0.0 Original release

(C) Copyright 2011 by Autodesk, Inc. 

Permission to use, copy, modify, and distribute this software in
object code form for any purpose and without fee is hereby granted, 
provided that the above copyright notice appears in all copies and 
that both that copyright notice and the limited warranty and
restricted rights notice below appear in all supporting 
documentation.

AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC. 
DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
UNINTERRUPTED OR ERROR FREE.
