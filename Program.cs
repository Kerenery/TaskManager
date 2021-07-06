using System;
using Spectre.Console.Cli;

namespace TaskManager
{
    class Program
    {
        static int Main(string[] args)
        {
            return SpectreIO.Command(args);
        }
    }
}

// / add task - info - создает новую задачу                                                                                  output progress
// /all - выводит все задачи                                                                                                 output progress
// /delete id - удаляет задачу по идентификатору (который должен отображаться в /all)                                        output progress
// / save file - name.txt - сохраняет все текущие задачи в указанный файл                                                    output 
// /load file-name.txt - загружает задачи с файла                                                                            output
// Указание, что задача выполнена                                                                                            done  
// /complete id - выставляет, что задача выполнена                                                                           output
// Выполненные задачи отображаются в конце списка и помечаются, что они выполнены                                            output
// /completed - выводит все выполненные задачи                                                                               output 
// Возможность указать дату выполнения (дедлайн)                                                                             done
// Информация вывродиться в /all                                                                                             output progress
// Добавляется команда /today - выводит только те задачи, которые нужно сделать сегодня                                      output
// Группировка задач - возможность создавать группы задач                                                                    done
// /create-group group-name - создает группу для задач                                                                       done
// /delete-group group-name - удаляет группу с заданным именем                                                               done
// /add-to-group id group-name - добавляет таску с указанным id в группу с указанным именем                                  done
// /delete-from-group id group-name - удаляет задачу c группы                                                                done
// Задачи, которые находятся в группе, должны при выполнении /all отображаться вложенным списком                             output 
// /completed group-name - выводит все выполненные в группе задачи                                                           output
// Подзадачи                                                                                                                 done
// Команда /add-subtask id subtask-info - добавляет к выбранной задаче подзадачу                                             done
// Добавить поддержку выполнения подзадачи по команде /complete id                                                           done  
// Для задач с подзадачами выводится информация о том, сколько подзадач выполнено в формате "3/4"                            output
// Обработка ошибок - отсутствие файлов, неправильный формат ввода                                                           done
// Учесть корнер кейсы. Например: задача не может быть добавлена дважды                                                      done