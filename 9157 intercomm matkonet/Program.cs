using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9157_intercomm_matkonet
{
    public class Key
    {
        private char press;
        private int sec;

        public Key(char press, int sec)
        {
            this.press = press;
            this.sec = sec;
        }

        public char GetPress() => press;
        public int GetSec() => sec;

        public override string ToString() => $"({press},{sec})";
    }



    public class Program
    {
        public static bool CheckN(Node<Key> l, Node<char> pwd)
        {
            int t = -1 * l.GetValue().GetSec();
            while (l != null && pwd != null)
            {
                if (l.GetValue().GetPress() != pwd.GetValue())
                    return false;
                t += l.GetValue().GetSec();
                l = l.GetNext();
                pwd = pwd.GetNext();
            }
            if (pwd == null)
                return t <= 5;
            return false;
        }

        static bool CheckPWD(Node<Key> l, Node<char> pwd)
        {
            while (l != null)
            {
                if (CheckN(l, pwd))
                    return true;
                l = l.GetNext();
            }
            return false;
        }



        //complexity - if n is length of input, m is length of pwd, then:
        //if length of m is arbitrary - O(n*m)
        //if m has size limit, say 5, then O(n)


        public static void Main()
        {
            Console.WriteLine("Running test cases for CheckPWD...\n");

            Node<char> pwd = BuildCharList(new char[] { '1', '5' });

            var input1 = BuildList(new Key[] { new Key('1', 1), new Key('5', 2) });
            Console.WriteLine("Test 1 - Basic Success (expected: true): " + CheckPWD(input1, pwd));

            var input2 = BuildList(new Key[] { new Key('1', 2), new Key('5', 6) });
            Console.WriteLine("Test 2 - Too Slow (expected: false): " + CheckPWD(input2, pwd));

            var input3 = BuildList(new Key[] { new Key('2', 1), new Key('9', 1), new Key('3', 1) });
            Console.WriteLine("Test 3 - No Match (expected: false): " + CheckPWD(input3, pwd));

            var input4 = BuildList(new Key[] { new Key('1', 2), new Key('5', 7), new Key('1', 2), new Key('5', 2) });
            Console.WriteLine("Test 4 - Multiple Sequences (expected: true): " + CheckPWD(input4, pwd));

            var input5 = BuildList(new Key[] { new Key('1', 2), new Key('5', 2) });
            Console.WriteLine("Test 5 - Time OK even if miscounted (expected: true): " + CheckPWD(input5, pwd));

            var input6 = BuildList(new Key[] { new Key('1', 3), new Key('5', 1) });
            Console.WriteLine("Test 6 - Also OK if miscounted (expected: true): " + CheckPWD(input6, pwd));

            var input7 = BuildList(new Key[] { new Key('1', 3), new Key('5', 3) });
            Console.WriteLine("Test 7 - Should fail if first sec counted (expected: true): " + CheckPWD(input7, pwd));

            var input8 = BuildList(new Key[] { new Key('9', 1), new Key('3', 1), new Key('1', 2), new Key('5', 2) });
            Console.WriteLine("Test 8 - Match at end (expected: true): " + CheckPWD(input8, pwd));

            // Change the password to 1→2→3 for this test
            Node<char> pwd123 = BuildCharList(new char[] { '1', '2', '3' });

            var input9 = BuildList(new Key[]
            {
    new Key('1', 1),
    new Key('1', 1),
    new Key('2', 1),
    new Key('3', 1)
            });

            Console.WriteLine("Test 9 - Overlapping Start (expected: true): " + CheckPWD(input9, pwd123));


        }

        public static Node<Key> BuildList(Key[] keys)
        {
            Node<Key> head = null, tail = null;
            foreach (Key k in keys)
            {
                var node = new Node<Key>(k);
                if (head == null)
                    head = tail = node;
                else
                {
                    tail.SetNext(node);
                    tail = node;
                }
            }
            return head;
        }

        public static Node<char> BuildCharList(char[] chars)
        {
            Node<char> head = null, tail = null;
            foreach (char c in chars)
            {
                var node = new Node<char>(c);
                if (head == null)
                    head = tail = node;
                else
                {
                    tail.SetNext(node);
                    tail = node;
                }
            }
            return head;
        }
    }
}
