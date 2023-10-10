using RedBlackTreeNotDel;
using System;

var redBlack = new RedBlackTreee();

var random = new Random();

var value = 3;

redBlack.Insert(34);
redBlack.Insert(84);

redBlack.Insert(15);
redBlack.Insert(0);
redBlack.Insert(2);
redBlack.Insert(79);
redBlack.Insert(99);
redBlack.Insert(9);
redBlack.Insert(88);
redBlack.Insert(89);
redBlack.Insert(18);
redBlack.Insert(31);
redBlack.Insert(39);
redBlack.Insert(100);
redBlack.Insert(101);



//var inicioRed = Environment.TickCount;
//for (int i = 0; i < 5000000; i++)
//{
//    int randomNumber = random.Next(0, 100000);

//    if(i == 4999999)
//    {
//        redBlack.Insert(value, i);
//        continue;
//    }

//    redBlack.Insert(randomNumber, i);
//}
//var fimRed = Environment.TickCount;

//var first2 = new Student();
//first2.SetName("Augusto ");
//first2.SetAge(24);
//first2.Registration = value;

//redBlack.Insert(first2);



//var studant = redBlack.Get(value);


Console.WriteLine($"{Seila.Value}");

//Console.WriteLine($"TempoRed {(fimRed - inicioRed) / 1000}");

static class Seila
{
    public static int Value = 0;
}
