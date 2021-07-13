using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexTA20V
{
    // Задание 1:
    // * Добавить в класс член "маска подсети"
    // * Реализовать присвоение маски через строку 255.255.255.0 либо 24
    //   обязательно проверять осмысленность маски, заданной через первый вариант
    // Задание 2:
    // * Реализовать возможность задавать маску прямо при присвоении IP-адреса
    //   например: 192.168.0.1/24
    //   При этом вариант без указания маски также допускается
    public class IPAddress
    {
        private byte[] octets = new byte[4];

        public string Address
        {
            get => $"{octets[0]}.{octets[1]}.{octets[2]}.{octets[3]}";
            set
            {
                if(ValidateIpString(value))
                {
                    string[] parts = value.Split('.');

                    for(int i=0; i<4;i++)
                    {
                        octets[i] = byte.Parse(parts[i]);
                    }
                }
            }
        }

        public byte GetOctet(int i)
        {
            if (i >= 4) throw new IndexOutOfRangeException("Octet index out of range");
            return octets[i];
        }

        public IPAddress()
        {
            for (int i = 0; i < 4; i++) octets[i] = 0;
        }

        public bool ValidateIpString(string s)
        {
            Regex r = new Regex(@"^(((1?\d{1,2})|(2(([0-4]\d)|(5[0-5]))))\.){3}(((1?\d{1,2})|(2(([0-4]\d)|(5[0-5])))))$");

            return r.IsMatch(s);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = new IPAddress();
            ip.Address = "192.168.0.1";

            Console.WriteLine($"IP: {ip.Address}");
            for(int i=0;i<4;i++)
            {
                Console.WriteLine($"Octet #{i}: {ip.GetOctet(i)}");
            }
            Console.ReadKey();
        }
    }
}
