================================================
       Plugin of the Month, November 2011
Brought to you by the Autodesk Developer Network
================================================
-------------------
Revit String Search
-------------------

Description
-----------
Search Revit project elements and their parameter values for a given
string.


System Requirements
-------------------
This plugin has been tested with Revit Architecture 2015, and requires
the .NET Framework 4.5.

The source code is included as a Visual Studio 2012 C# project.
It is not required to run the plugin.


Installation
------------
The following steps are for using the plugin with Revit 2015.

1. If you are using Vista or Windows 7, first check whether the zip
file needs to be unblocked. Right-click on the zip file and select
"Properties". If you see an "Unblock" button, then click it.

2. Copy the plugin module "ADNPlugin-StringSearch.dll" and the add-in
manifest file "ADNPlugin-StringSearch.addin" to one of the following
locations:

  C:\Users\<your login>\AppData\Roaming\Autodesk\Revit\Addins\2015

or

  C:\ProgramData\Autodesk\Revit\Addins\2015

The first location will make the plugin available for your use only,
while the second is for all users of your computer.

** Note: The first location is recommended if you have security
permission issues (e.g., when UAC is turned on).

If you decide on a different location for the DLL, please modify the
following line in the add-in manifest file
"ADNPlugin-StringSearch.addin" to match your desired location:

  <Assembly>.\ADNPlugin-StringSearch.dll</Assembly>

3. Once installed, the "String Search" command becomes available in
Revit.  Go to the "Add-Ins" tab > "String Search" panel. It shows
the "String Search" button to launch the command.


Usage
-----
In Revit, click "Add-Ins" > "String Search" panel > "String Search"
to launch the command.

In the dialogue that appears, you can type in the search string in
the 'Find what' text box.

Click 'OK'. The command will search for the given string on all
standard and user parameters of all elements in the current view.

All occurrences of the search string are listed in a data grid view
in a modeless navigator dialogue. Double click on a row in the
navigator to zoom to the element.

A log file of the search operation and its results is created in
your temporary directory. To view the log file, right click on the
search form and select 'Display Log File'.

The string matching options and the elements and parameters to
search can be modified as follows.


Options
-------
Category: Limit the search to elements belonging to a specific
category. All other categories will be skipped. Specify an
asterisk '*' to search all categories. This is the default setting.

Parameter name: You can specify the name of a specific parameter
to search in. All other parameters will be skipped. Specify an
asterisk '*' to search all parameters. This is the default setting.

Find options: Match case and match whole parameter limit the string
pattern matching to more specific cases, i.e. the upper and lower
case of each character must match, and the specified search string
must match the entire parameter value.

Element selection: You can search either the currently selected
element set, all elements in the current view, or all elements
in the entire project. The latter can be slow.

Instances versus Types and Symbols: You can specify whether to
search in the BIM elements themselves, such as walls and family
instances, or in the element types, such as wall types and family
symbols. When searching element types, there is no way to limit the
search to selected elements (because they cannot be selected) or to
a view (because they are not displayed graphically). By default, all
elements in the current view are searched. Searching types is not
additional to instances, it is complementary. You can search either
element types (derived from ElementType in the API) or instances,
but not both at once.

Parameter selection: You can select whether only standard built-in
Revit parameters are searched, or user-defined family and shared
parameters, or both.

Advanced: You have the option of using a regular expression to
specify the search string. This option is intended for users with
advanced programming knowledge and is not suited for non-
programmers. The .NET RegEx library is used for this.
For more detail about regular expression, please refer to the
following link to a 'cheat sheet':

  http://regexlib.com/cheatsheet.aspx

You can also search in BuiltInParameters, which are only visible
through the Revit API. Again, this option is intended for users with
advanced programming knowledge and is not suited for others.
With this option, the parameter selection option is ignored and
the 'Parameter name' setting switches to a list of all built-in
parameters. Again, you can search in all of them using the asterisk
'*', or pick a specific one. Searching all built-in parameters
across the entire project can be extremely slow.


Examples
--------
To search for a string 'abc' in all parameter values of all elements
in the current view, simply type in 'abc' as the search string and
hit OK. If any occurrences are found, they are listed in the
modeless navigator window. You can continue working in Revit as
normal. If you double click an entry in the navigator, the Revit
screen will zoom in to the selected element and highlight it.

To search for all structurally non-bearing walls, you might make use
of the built-in parameter option as follows:

- Enter the search string 'Non-bearing'
- Under Category, select Walls
- Under Advanced, select 'Search BuiltInParameters'
- Under Built-in parameter, select WALL_STRUCTURAL_USAGE_TEXT_PARAM

Click OK and examine the results in the navigator.


Uninstallation
--------------
Simply removing "ADNPlugin-StringSearch.addin" file from your
installation folder will uninstall the plugin.


Limitations and Known Issues
----------------------------
None.


Author
------
This plugin was written by Jeremy Tammik of the Autodesk Developer
Technical Services team.


Acknowledgements
----------------
Many thanks to Saikat Bhattacharya for his careful review and many
valuable suggestions!


Further Reading
---------------
For more information on developing with Revit, please visit the
Revit Developer Center at http://www.autodesk.com/developrevit.


Feedback
--------
Email us at labs.plugins@autodesk.com with feedback or requests for
enhancements.


Release History
---------------
1.0  Original release
2.0  Migrated to Revit 2013
3.0  Migrated to Revit 2014
2015.0.0.0 Migrated to Revit 2015

Future Enhancement Wishes
-------------------------
- Implement a progress bar for long operations (e.g. searching all
  built-in parameters on all elements).

- Support for search and replace.


(C) Copyright 2011-2014 by Autodesk, Inc.

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
