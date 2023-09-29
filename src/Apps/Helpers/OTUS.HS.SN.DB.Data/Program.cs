// See https://aka.ms/new-console-template for more information

using OTUS.HS.SN.DB.Data;
using PowerArgs;

await Args.InvokeActionAsync<ProgramActions>(args);

Console.WriteLine("Done");
Console.ReadKey();
