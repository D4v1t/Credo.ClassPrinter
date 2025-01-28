// See https://aka.ms/new-console-template for more information

using Credo.ClassPrinter.BusinessLogic.Services;
using Credo.ClassPrinter.Models;

var printer = new RecursiveObjectPrinter();

Console.WriteLine(printer.Print(typeof(CircularClass)));

Console.WriteLine(printer.Print(typeof(ClassWithProperties)));

Console.WriteLine(printer.Print(typeof(ClassWithPropertiesAndNestedTypes)));


Console.ReadLine();