using System;
using System.Collections;
using System.Numerics;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Collections.Specialized;

namespace CP
{
    class Solution
    {
        // some useful methods
        bool IsPrime(long n)
        {
            if (n == 2 || n == 3) return true;
            if (n % 2 == 0) return false;
            for (long i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        List<long> FindPrimeFactors(long n)
        {
            List<long> primeFactors = new List<long>();
            while (n % 2 == 0)
            {
                primeFactors.Add(2);
                n /= 2;
            }
            for (long i = 3; i * i <= n; i += 2)
            {
                while (n % i == 0)
                {
                    primeFactors.Add(i);
                    n /= i;
                }
            }
            if (n > 1) primeFactors.Add(n);
            return primeFactors;
        }

        List<long> FindAllDivisors(long n)
        {
            List<long> allDivisors = new List<long>();
            for (long i = 1; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    allDivisors.Add(i);
                    if (n / i != i) allDivisors.Add(n / i);
                }
            }
            return allDivisors;
        }

        long FirstNSum(long n)
        {
            return n * (n + 1) / 2;
        }

        /// sieve ---------------------------
        const int MAX = 10000005;
        int[] primes = new int[MAX];
        List<int> allPrimes = new List<int>();
        void Sieve()
        {
            primes = Enumerable.Repeat(0, MAX).ToArray();
            primes[0] = primes[1] = 1;

            // for multiple of 2
            for (int i = 4; i < MAX; i += 2)
            {
                primes[i] = 1;
            }

            // for other primes
            for (int i = 3; i * i < MAX; i += 2)
            {
                if (primes[i] == 0)
                {
                    for (int j = i * i; j < MAX; j += i)
                    {
                        primes[j] = 1;
                    }
                }
            }

            // taking all primes into list
            for (int i = 2; i < MAX; i++)
            {
                if (primes[i] == 0)
                {
                    allPrimes.Add(i);
                }
            }
        }

        bool IsPalindrome(string s)
        {
            int n = s.Length;
            for (int i = 0; i < n / 2; i++)
            {
                if (s[i] != s[n - 1 - i]) return false;
            }
            return true;
        }

        long Power(long n, long p)
        {
            if (p == 0) return 1;
            if (p == 1) return n;
            long ans = Power(n, p / 2);
            ans *= ans;
            if (p % 2 != 0) ans *= n;
            return ans;
        }

        long ModPower(long n, long p, long mod)
        {
            if (p == -1) p = mod - 2;
            if (p == 0) return 1;
            if (p == 1) return n;
            long ans = ModPower(n, p / 2, mod);
            if (p % 2 != 0)
            {
                n %= mod;
                ans *= n;
                ans %= mod;
            }
            ans *= ans;
            ans %= mod;
            return ans;
        }
        long FindGCD(long x, long y)
        {
            if (x % y == 0) return y;
            return FindGCD(y, x % y);
        }

        string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }

        void SwapValue(ref long x, ref long y)
        {
            x = x ^ y;
            y = x ^ y;
            x = x ^ y;
        }

        long CountDigits(long x)
        {
            long ans = 0;
            while (x > 0)
            {
                ans++;
                x /= 10;
            }
            return ans;
        }

        // ---------------------------
        ///  bitmask related things
        /// pos is 0 based index and starts from left to right: [32 31 30 ... ... ... 3 2 1 0]

        long OnBit(long N, int pos)
        {
            return N | (1L << pos);
        }
        long OffBit(long N, int pos)
        {
            return N & ~(1 << pos); /// ~ means flip the bits
        }
        long flipbit(long N, int pos)
        {
            return N ^ (1L << pos);
        }
        bool checkbit(long N, int pos)
        {
            long bit = N & (1L << pos);
            return bit == 0 ? false : true;
        }

        public void Yes() { Console.WriteLine("Yes"); }
        public void No() { Console.WriteLine("No"); }

        public List<int> ScanIntList()
        {
            List<string> stringList = Console.ReadLine().Split(' ').ToList();
            List<int> intList = stringList.Select(x => Convert.ToInt32(x)).ToList();
            return intList;
        }
        public List<long> ScanLongList()
        {
            List<string> stringList = Console.ReadLine().Split(' ').ToList();
            List<long> longList = stringList.Select(x => Convert.ToInt64(x)).ToList();
            return longList;
        }

        /// How can I use a pair?
        /// Tuple<string, int> t = new Tuple<string, int>("Hello", 4);
        /// List<(int, int)> list = new List<(int, int)> ();
        /// KeyValuePair can be used also!

        /// something like fill/memset in c++, for initialize an n sized array/vector:
        /// List<int> a = Enumerable.Repeat(100, n).ToList();


        /// for flipping use ^ with 1
        /// For DP: state, transition, base, return, memorization

        // main code goes here ----------------------------------------------------
        const long N = 1000 * 1000;
        long mod = 1000 * 1000 * 1000 + 7;

        public void Solve()
        {
            int n = ScanIntList()[0];
            List<int> a = ScanIntList();
            SortedDictionary<int, int> map = new SortedDictionary<int, int>();
            foreach (var x in a)
            {
                if (map.ContainsKey(x))
                {
                    map[x]++;
                }
                else map[x] = 1;
            }

            int ans = 0;

            for (int i = 0; i < n; i++)
            {
                if (map.Count == n - i)
                {
                    ans = i;
                    break;
                }
                else
                {
                    if (map.ContainsKey(a[i]))
                    {
                        map[a[i]]--;
                        if (map[a[i]] == 0)
                        {
                            map.Remove(a[i]);
                        }
                    }
                }
            }

            Console.WriteLine(ans);
        }

        // ------------------------------------------------------------------------
        public void Init()
        {
            int testCase = 1;
            testCase = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= testCase; i++)
            {
                //Console.Write("Case " + i + ": ");
                Solve();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            solution.Init();
        }
    }
}