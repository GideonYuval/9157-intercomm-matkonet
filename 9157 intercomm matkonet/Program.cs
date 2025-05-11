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
        public static bool CheckN(Node<Key> l, Node<char> pwd, int time)
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
                return t <= time;
            return false;
        }

        static bool CheckPWD(Node<Key> l, Node<char> pwd, int time) //my version
        {
            while (l != null)
            {
                if (CheckN(l, pwd, time))
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

            int passed = 0;
            int total = 0;

            Node<char> pwd = BuildCharList(new char[] { '1', '5' });
            int timeLimit = 5;

            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[] { new Key('1', 1), new Key('5', 2) }), pwd, timeLimit), true, "Basic Success");

            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[] { new Key('1', 2), new Key('5', 6) }), pwd, timeLimit), false, "Too Slow");

            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[] { new Key('2', 1), new Key('9', 1), new Key('3', 1) }), pwd, timeLimit), false, "No Match");

            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[] { new Key('1', 2), new Key('5', 7), new Key('1', 2), new Key('5', 2) }), pwd, timeLimit), true, "Multiple Sequences");

            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[] { new Key('1', 2), new Key('5', 2) }), pwd, timeLimit), true, "Time OK even if miscounted");

            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[] { new Key('1', 3), new Key('5', 1) }), pwd, timeLimit), true, "Also OK if miscounted");

            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[] { new Key('1', 3), new Key('5', 3) }), pwd, timeLimit), true, "Should fail if first sec counted (miscounted test)");

            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[] { new Key('9', 1), new Key('3', 1), new Key('1', 2), new Key('5', 2) }), pwd, timeLimit), true, "Match at end");

            // Test 9 - Overlapping Start
            Node<char> pwd123 = BuildCharList(new char[] { '1', '2', '3' });
            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[]
            {
        new Key('1', 1),
        new Key('1', 1),
        new Key('2', 1),
        new Key('3', 1)
            }), pwd123, timeLimit), true, "Overlapping Start");

            Console.WriteLine($"\nResult: {passed}/{total} tests passed");
        }

        static int RunTest(int testNumber, bool actual, bool expected, string description)
        {
            string result = (actual == expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Test {testNumber} - {description}: expected {expected}, got {actual} → {result}");
            return (actual == expected) ? 1 : 0;
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
