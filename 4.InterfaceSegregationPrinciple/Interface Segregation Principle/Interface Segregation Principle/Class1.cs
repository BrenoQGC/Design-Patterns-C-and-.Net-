using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Interface should be segregate so you dont have to implement functions they dont actually need.
//Break the interface by smaller interfaces

namespace Interface_Segregation_Principle
{
    public class Document
    {
    }

    //Generic Interface
    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Print(Document d)
        {
            //Print function
        }

        public void Scan(Document d)
        {
            //Scan function
        }

        public void Fax(Document d)
        {
            //Fax function
        }
    }

    //
    public class OldfashionedPrinter : IMachine
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            //Printer cannot Scan
        }

        public void Fax(Document d)
        {
            //Printer cannot fax
        }
    }

    //Create different interfaces for different devices functionality 
    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }

        //Interfaces can inherit from other interfaces
        public interface IMultiFunctionDevice : IScanner, IPrinter
        {

        }

        public class MultiFunctionMachine : IMultiFunctionDevice
        {

            private IPrinter printer;
            private IScanner scanner;

            //You can do delegation just implementing the missing member of the interfaces
            //Or initializes (create constructor) the interface methods 
            //and delegate (alt+ins > Delegate members) then to the printer and scanner
            public MultiFunctionMachine(IPrinter printer, IScanner scanner)
            {
                this.printer = printer;
                this.scanner = scanner;
            }

            public void Print(Document d)
            {
                printer.Print(d);
            }

            public void Scan(Document d)
            {
                scanner.Scan(d);
            }
        }

        class Class1
        {
        }
    }
}
