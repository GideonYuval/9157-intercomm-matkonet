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

        static bool CheckPWD1(Node<Key> l, Node<char> pwd, int time) //my version
        {
            while (l != null)
            {
                if (CheckN(l, pwd, time))
                    return true;
                l = l.GetNext();
            }
            return false;
        }

        //student code

        static bool CheckPWD(Node<Key> k, Node<char> p, int t)
        {
            Node<char> head = p;
            int sum = 0;
            bool streak = false;

            while (k != null)
            {
                if (p == null && sum <= t)
                    return true;
                else if (p == null && sum > t)
                {
                    sum = 0;
                    streak = false;
                    p = head;
                }

                if (streak)
                    sum += k.GetValue().GetSec();

                if (p.GetValue() == k.GetValue().GetPress())
                {
                    streak = true;
                    p = p.GetNext();
                }
                else if (p.GetValue() != k.GetValue().GetPress())
                {
                    sum = 0;
                    streak = false;
                    p = head;
                }

                k = k.GetNext();
            }

            return false;
        }




        //end student code





        //complexity - if n is length of input, m is length of pwd, then:
        //if length of m is arbitrary - O(n*m)
        //if m has size limit, say 5, then O(n)


        public static void Main()
        {
            int passed = 0;
            int total = 0;
            int timeLimit = 5;

            // Test 1 – Basic match in the middle
            Node<char> pwd1 = BuildCharList(new char[] { '1', '5' });
            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[]
            {
        new Key('1', 1),
        new Key('5', 2),
        new Key('9', 1)  // extra key after match
            }), pwd1, timeLimit), true, "Basic match: password occurs before end of input");

            // Test 2 – Almost match, still valid if first sec miscounted
            Node<char> pwd2 = BuildCharList(new char[] { '1', '5' });
            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[]
            {
        new Key('1', 3),
        new Key('5', 2),
        new Key('9', 1)
            }), pwd2, timeLimit), true, "Almost match: forgiving if first sec is miscounted");

            // Test 3 – No match at all
            Node<char> pwd3 = BuildCharList(new char[] { '1', '2' });
            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[]
            {
        new Key('9', 1),
        new Key('3', 2),
        new Key('5', 1)
            }), pwd3, timeLimit), false, "No match: password does not appear at all");
            
            // Test 4 – Match is too slow, even if miscounted
            Node<char> pwd4 = BuildCharList(new char[] { '1', '5' });
            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[]
            {
        new Key('1', 1),
        new Key('5', 6),
        new Key('8', 1)
            }), pwd4, timeLimit), false, "Time overflow: too slow even if miscounted");
            
            // Test 5 – Match at the very end
            Node<char> pwd5 = BuildCharList(new char[] { '1', '2' });
            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[]
            {
        new Key('9', 1),
        new Key('3', 1),
        new Key('1', 1),
        new Key('2', 1)
            }), pwd5, timeLimit), true, "Password match occurs at the end of the input");

            // Test 6 – Overlapping start (e.g., 1 1 2 3, pwd = 1 2 3)
            Node<char> pwd6 = BuildCharList(new char[] { '1', '2', '3' });
            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[]
            {
        new Key('1', 1),
        new Key('1', 1),
        new Key('2', 1),
        new Key('3', 1),
        new Key('9', 1)
            }), pwd6, timeLimit), true, "Overlapping match: restarts during sequence");

            // Test 7 – Empty password
            Node<char> pwd7 = null;
            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[]
            {
        new Key('1', 1),
        new Key('2', 1)
            }), pwd7, timeLimit), false, "Empty password: should return false");

            // Test 8 – Input shorter than password
            Node<char> pwd8 = BuildCharList(new char[] { '1', '2', '3' });
            total++;
            passed += RunTest(total, CheckPWD(BuildList(new Key[]
            {
        new Key('1', 1),
        new Key('2', 1)
            }), pwd8, timeLimit), false, "Input shorter than password: cannot match");

            // Final summary
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
