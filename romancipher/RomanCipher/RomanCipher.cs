using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace UML.Assignment5
{
   
    class RomanCipherWriter : StreamWriter   //RomanCipherWRiter class, inherits from StreamWriter
    {
        public RomanCipherWriter(string path1, int num1)  // constructor accepts string file path information & int info regarding how many units to shift 
            : base(path1)
        {
            

            if (num1 <= 0)  //exit if int num1 < or = to 0
            {
                Console.WriteLine("Shift unit must be a positive number.  Press enter to exit. ");
                Console.ReadLine();
                Environment.Exit(0);
            }

            path = path1;
            num = num1;

        }

        
        public override void Write(string value)  //writes text entered by user to file in Roman Cipher code form
        {
           
            int shift = num;  //shifting characters  
            

            string fPath = path;  //file path
         
            


            char[] buffer = value.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                // Letter.
                char letter = buffer[i];
                string lett = letter.ToString();
                
                //does not preform any shifting on non letter characters, simply stores in array
         
                if (lett != "A" && lett != "B" && lett != "C" && lett != "D" && lett !="E" && lett != "F" && lett != "G" && lett != "H" && lett != "I" && lett !="J" && lett != "K" && lett != "L" && lett != "M" && lett != "N" && lett !="O" && lett != "P" && lett != "Q" && lett != "R" && lett != "S" && lett !="T" && lett != "U" && lett != "V" && lett != "W" && lett != "X" && lett !="Y" && lett != "Z" )
                {
                    
                    goto Even;
                }
               
                // Add shift to all non letters.
                
             
                letter = (char)(letter - shift);   
                
                // Subtract 26 on over
                // Add 26 on under
               
                if (letter > 'Z')
                {
                    letter = (char)(letter - 26);
                }
                else if (letter < 'A')
                {
                    letter = (char)(letter + 26);
                }
                
            Even:  // jump here for non letter characters
                buffer[i] = letter;  //store data in array

           
          
            }
            Console.WriteLine();
            Console.WriteLine("Roman Cipher converted text:");

            //writes converted text to file and on screen
            for (int i = 0; i < buffer.Length; i++)
            {
                base.Write(buffer[i]);
                Console.Write(buffer[i]);
            }


            
        }

        private string path;
        private int num;



    }

    class RomanCipherReader : StreamReader  //RomanCipherReader class inherits from StreamReader
    {
        public RomanCipherReader(string path1, int num1)  //accepts file path and int argument for alphabet shifting
            : base(path1)
        {
            path = path1;
            num = num1;

        }




        public override string ReadToEnd()  //override ReadToEnd() for reading and decrypting input stream
        {
            
            Console.WriteLine();
            string value = "0";
            int shift = -num; //# shift to

            

          value =  base.ReadToEnd();
            char[] buffer = value.ToCharArray();
       
            for (int i = 0; i < buffer.Length; i++)
            {
                
                char letter = buffer[i];
                string lett = letter.ToString();

                //does not preform any shifting on non letter characters
               

                if (lett != "A" && lett != "B" && lett != "C" && lett != "D" && lett != "E" && lett != "F" && lett != "G" && lett != "H" && lett != "I" && lett != "J" && lett != "K" && lett != "L" && lett != "M" && lett != "N" && lett != "O" && lett != "P" && lett != "Q" && lett != "R" && lett != "S" && lett != "T" && lett != "U" && lett != "V" && lett != "W" && lett != "X" && lett != "Y" && lett != "Z")
                
                {
                    //Jump for non letter characters to simply store in array...no shiffting
                    goto Even;
                }






             
                letter = (char)(letter - shift);
                // Subtract 26 on overflow.
                // Add 26 on underflow.                     //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
               
                if (letter > 'Z')
                {
                    letter = (char)(letter - 26);
                }
                else if (letter < 'A')
                {
                    letter = (char)(letter + 26);
                }
                // Store.  
            Even:
                buffer[i] = letter;
                


            }

            Console.WriteLine();
            Console.WriteLine("Text converted back to original from:");


            for (int i = 0; i <buffer.Length; i++)
            {
                
                Console.Write(buffer[i]);
               
            }

         //  


           return new string(buffer);  //simply returning because override ReadToEnd has string return value
           // return;
        }


        //return base.ReadToEnd();

        private string path;
        private int num;



    }


    class HeaderFile : StreamReader  //HeaderFile class inherits from StreamReader 
    {
        public HeaderFile(string path1, int num1)  //file path to read from and int num1 to read first "num1" number of lines
            : base(path1)
        {
            if (num1 <= 0)
            {
                Console.WriteLine("This must be a positive number, press enter to exit. ");
                Console.ReadLine();
                Environment.Exit(1);
            }

            path = path1;
            num = num1;



        }

        public override string ReadToEnd()   //override
        {
            return path;
        }

        public int count()  //method to count how many lines in file.  
        {
            int cnt = 0;
            while (ReadLine() != null)  //while line in file is not null, increment counter
            {
                cnt++;
            }
            return cnt;  //return cnt value to main
        }

        public string[] ReadLines()  //method to read number of lines specified
        {
           

            howManyReadLines = num;
            int cnt = 0;
            string[] arr = new string[howManyReadLines + 2];  //added two so array  will not be out of bounds
          

            while (cnt <= howManyReadLines)
            {
                arr[cnt] = ReadLine();
                cnt++;
            }
            
            return arr;

        }

        public new Stream BaseStream  //prevents callers from accessing basestream property...returning null
        {
            
            get
            {
                return null;
            }
        }

        private string path;
        private int num;
        private int howManyReadLines;

        class TailFile : StreamReader   //TailFile class also inherits from Stream Reader
        {
            public TailFile(string path1, int num1)  //file path  
                : base(path1)
            {

                path = path1;
                num = num1;
                nNumOfLinesToRead = num1;


            }

            public string[] ReadLines()
            {
               
                int cnt = 0;
                int cnt2 = 0;
                int arrSize = num + 2;  //prevent out of index in array error
                string[] arr = new string[arrSize];
                string[] arr2 = new string[arrSize];
                howManyReadLines = num ;
                

                while (!EndOfStream )  //
                {
                    arr[cnt] = ReadLine();
                   
                    cnt++;
                }


                while (cnt2 <= howManyReadLines)  //storing last number/N lines in array
                {
                    arr2[cnt2] = arr[cnt];
                    cnt2++;
                    cnt --;

                }

               // Console.WriteLine("{0}", cnt);
                return arr2;

            }







            private int nNumOfLinesToRead;
            private int howManyReadLines;
            private string path;
            private int num;

        }



        class Program
        {
            static void Main(string[] args)
            {
                int counter = 0;
                
                int howMany = 0;
                Console.WriteLine("How many lines should be read in file created by Header File class?" );
                string n = Console.ReadLine();
                int N;
                int.TryParse(n, out N);

            

              HeaderFile cf = new HeaderFile(@"c:\temp\test.txt", N);  //count number of lines in file
              int total = cf.count();
              cf.Close();

              HeaderFile hf = new HeaderFile(@"c:\temp\test.txt", N); // file path & read N number of lines
              TailFile tf = new TailFile(@"c:\temp\test.txt", total); //total = total number of lines in file, ref point for reading last ine in file

           
             
 

                string[] lines = hf.ReadLines();

                while (counter < N && lines[counter] != null)
                {
                    Console.WriteLine(lines[counter]);
                    counter++;
                }
                Console.WriteLine();

                Console.WriteLine("................................................................");

                Console.WriteLine("How many of the last 'N' lines do you want to read from the file?");
                string anotherN = Console.ReadLine();
                int.TryParse(anotherN, out howMany);

                if (howMany > total)
                {
                    howMany = total;
                 
                }

               // howMany = 2;

                if (howMany <= 0)
                {
                    Console.WriteLine("This must be a positive number, press enter to exit");
                    Console.ReadLine();
                    Environment.Exit(1);
                }
                
                string[] lines2 = tf.ReadLines();
              

                while ( howMany > 0)
                {
                    Console.WriteLine(lines2[howMany]);
                    howMany--;
                  
                }



          

                Console.WriteLine();






               var x= hf.BaseStream;
           

            

              
                RomanCipherWriter rcw = new RomanCipherWriter(@"c:\temp\test2.txt", 3);  //shifts 3 units

                Console.WriteLine("Enter a string to convert");
                
              //  string doo = "9HELLO WORL!D";

               string doo = Console.ReadLine();
               string dup = doo.ToUpper();

               


                rcw.Write(dup);
                rcw.Close();


                  RomanCipherReader rcr = new RomanCipherReader(@"c:\temp\test2.txt", 3);  //file path and number of units to shift
                  rcr.ReadToEnd();
                  rcr.Close();


                  Console.WriteLine();
                  Console.WriteLine("Press enter to exit...");

                Console.ReadLine();



            }
        }
    }
}
