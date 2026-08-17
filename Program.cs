using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var currentFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
const string Id7FileName = "id7";
const int MaxId7 = 7;
var id7File = Path.Combine(currentFolder, Id7FileName);

var id7Tasks = File.Exists(id7File) switch
{
    true => (await File.ReadAllLinesAsync(id7File)).Take(MaxId7).ToList(),
    _ => new List<string>()
};

Action<string> output = x => Console.WriteLine(x);

async void SaveTasks() => await File.WriteAllLinesAsync(id7File, id7Tasks);

var inputData = args.Length > 0 ? string.Join(" ", args): string.Empty;

if (inputData == "?" || inputData == "--help" || inputData == "-h")
{
    output("Provide any type of string as a parameter to add a task.");
    output("BUT! Any string that represents number from 0-6 will be used to delete existing task.");
    output("Max number of tasks is 7.");
    return;
}

if (!string.IsNullOrEmpty(inputData))
{
    if (int.TryParse(inputData, out var id))
    {
        if (id < id7Tasks.Count && id >= 0)
        {
            output($"Deleting: {id7Tasks[id]}");
            output("-----------------");
            id7Tasks.RemoveAt(id);
            SaveTasks();
        }
        else
        {
            output("Can't find provided index.");
        }
    }
    else if (id7Tasks.Count >= 7)
    {
        output("More than 7 tasks. Delete one.");
    }
    else
    {
        id7Tasks.Insert(0, inputData);
        SaveTasks();
    }
}

if (id7Tasks.Count == 0)
{
    output("No tasks.");
    return;
}
for (var i = 0; i < id7Tasks.Count; i++)
{
    output($"{i}: {id7Tasks[i]}");
}
