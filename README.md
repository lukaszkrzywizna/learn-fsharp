# learn-fsharp
Code exercises for F# workshops

# Requirements:

In order to run and use repository, you need to have a few things:
1. Installed latest .Net 6 SDK https://dotnet.microsoft.com/en-us/download/visual-studio-sdks
2. IDE (one of) which supports F#:
   * Rider with F# plugin [LINUX & WINDOWS & MAC] https://www.jetbrains.com/rider/
   * Visual Studio 2022 [WINDOWS] https://visualstudio.microsoft.com/pl/vs/community/
   * Visual Studio Code with .Net Pack Extension [LINUX & WINDOWS & MAC] https://code.visualstudio.com/ https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-pack

# Instruction

1. Fetch the repository and checkout to start branch - `tasks/1-scrabble`
2. Get familiar with `learn_fsharp.pdf` presentation. It shows basic concepts of a functional thinking.
3. Please read `Lesson.fs` file to get know the basics of the language. Most of the expressions can be easily processed with dotnet fsi tool https://docs.microsoft.com/en-us/dotnet/fsharp/tools/fsharp-interactive/. Whole solution can be build with `dotnet build` command or by IDE.
4. `Tasks` folder contains simple tasks to resolve.
5. After compleation of first task, please move to another one, using git checkout. Every branch has number in its name reflecting the execution order. Next branch contain another task + solution for a previous one.
6. From the branches including name `feature`, you will have to develop more real-life solution located in `Enterprise` folder.
7. The last branch `feature/9-finito` contains solutions for all tasks and features.

Have fun!