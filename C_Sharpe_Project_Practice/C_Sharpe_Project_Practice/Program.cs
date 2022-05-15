using System;



namespace C_Sharpe_Project_Practice
{
    
   
  public   class Program 
    {
       
        static void Main(string[] args)
        {
            string a, b;
            Split("Sandip Kumer Mistry", out a, out b);
            Console.WriteLine(a);
            Console.WriteLine(b);
            
        }
        static void Split( string name,out string firstname,out string lastname)
        {
            int i = name.LastIndexOf(' ');
            firstname = name.Substring(0, i);
            lastname = name.Substring(i + 1);

        }


    }
  
}
