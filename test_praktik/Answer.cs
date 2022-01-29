using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Answer
{
    public int Index { get; set; }
    public int GoodAnswer { get; set; }
    public int SelectAnswer { get; set; }
}
public class Test
{ 
    public List<Answer> Answers { get; set; }
    public int MaxAnswers { get; set; }
    public int AnswersCount { get; set; }
    public int GoodAnsers { get; set; }
    public bool End { get; set; }
}

