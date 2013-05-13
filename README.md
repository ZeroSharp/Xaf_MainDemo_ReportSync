## Experiments with T4 to simplify working with DevExpress XAF reports ##

[DevExpress XAF](http://www.devexpress.com/Products/NET/Application_Framework/) supports a fully featured report writer for creating XafReports. XafReports are a subclass of [XtraReports](http://www.devexpress.com/Products/NET/Reporting/) but with some limitations. The most frustrating limitations of these are:

XtraReports are created and edited within Visual Studio. XafReports can only be edited by launching the WinForms client application.

XtraReports are saved as cs files - any scripts are compiled at compile-time. XafReports are saved as XML in a .repx file - any scripts are encoded and compiled at runtime. The repx files cannot be merged easily. Any errors in the scripts are discovered only at runtime. Writing shared code is difficult.

The goal of this project is to provide a two-way translation between XAF's repx files and XtraReport's .cs and .designer.cs files. The solution relies on the Visual Studio T4 templating platform to provide the conversion.

### Dependencies ###

Requires [DevExpress Xaf](http://www.devexpress.com/Products/NET/Application_Framework/). This currently uses version 12.2.8.