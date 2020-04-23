using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


//The Dependency Inversion Principle states that high level parts of the system should not depend
//on low level parts of the system directly
//instead they should depend on some kind of abstraction

//query genealogy database to define relationships
namespace Dependency_Inversion_Principle
{

    public enum Relationship // An enum is a special "class" that represents a group of constants 
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
        //public DateTime DateOfBirth;
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    //low-level parts:
    public class Relationships : IRelationshipBrowser
    {
        // person, type of relationship, person wich it applies
        private List<(Person, Relationship, Person)> relations
            = new List<(Person, Relationship, Person)>();

        //API find relationship
        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        //access relationship information for search
        public List<(Person, Relationship, Person)> Relations => relations;
        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations
                .Where(x => x.Item1.Name == name
                            && x.Item2 == Relationship.Parent).Select(r => r.Item3);
            //LINQ expression from
            //
            //foreach (var r in relations.Where(
            //    x => x.Item1.Name == name &&
            //         x.Item2 == Relationship.Parent))
            //{
            //    yield return r.Item3;
            //}
            //
        }
    }

        //High level research module dependent from low level parts

        //create a level of abstraction where relationships can change its way to store data defining a interface (IRelationshipBrowser)
        public class Research
    {
        //wrong method :
        //
        //public Research(Relationships relationships)
        //{
        //    var relations = relationships.Relations;
        //    foreach (var r in relations.Where(
        //        x => x.Item1.Name == "John" &&
        //             x.Item2 == Relationship.Parent
        //    ))
        //    {
        //        WriteLine($"John has a child called {r.Item3.Name}");
        //    }
        //}
        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
            {
                WriteLine($"John has a child called {p.Name}");
            }
        }
        static void Main(string[] args)
        {
            var parent = new Person {Name = "John"};
            var child1 = new Person {Name = "Chris"};
            var child2 = new Person {Name = "Mary"};

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }

    }
}
