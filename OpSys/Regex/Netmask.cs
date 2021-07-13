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
        private uint netmask = 24;

        public string Address
        {
            get => $"{octets[0]}.{octets[1]}.{octets[2]}.{octets[3]}";
            set
            {
                if(ValidateCombinedIpNetmask(value))
                {
                    string[] ip_nm = value.Split('/');

                    netmask = uint.Parse(ip_nm[1]);

                    StrAssignIp(value);
                }

                if(ValidateIpString(value))
                {
                    StrAssignIp(value);
                }
            }
        }

        private void StrAssignIp(string s)
        {
            string[] parts = s.Split('.');

            for (int i = 0; i < 4; i++)
            {
                octets[i] = byte.Parse(parts[i]);
            }
        }

        public string Netmask
        {
            get => netmask.ToString();
            set
            {
                // Если строка похожа на IP
                if(ValidateIpString(value))
                {
                    // проверяем вариант типа 255.255.255.0
                    //byte[] possible_octets = new byte[4];

                    string[] parts = value.Split('.');

                    uint sum = 0;

                    // Проходим по всем компонентам "маски" в октетном виде
                    for (int i = 0; i < 4; i++)
                    {
                        // интерпретируем компонент (строку) как байтовое значение
                        byte possible_octet = byte.Parse(parts[i]);
                        // если не все биты-единички в числе идут подряд - значит
                        // это не корректная маска, отмена присвоения
                        if(!AllOneBitsConsecutive(possible_octet))
                        {
                            return;
                        }
                        // в ином случае прибавляем к общей сумме все единички в октете (1-8)
                        sum += CountOneBits(possible_octet);
                    }

                    // маска = сумма всех битов-единичек, идущих подряд от начала
                    netmask = sum;
                }
                else if(ValidateNetmask(value))
                {
                    netmask = uint.Parse(value);
                }
            }
        }

        private uint CountOneBits(byte b)
        {
            uint sum = 0;
            byte bitmask = 1;
            for (int i = 0; i < 8; i++, bitmask <<= 1)
            {
                if ((b & bitmask) == 0) sum++;
            }
            return sum;
        }

        private bool AllOneBitsConsecutive(byte b)
        {
            bool result = true;
            byte bitmask = 1;
            bool zero_encountered = false;
            for (int i = 0; i < 8; i++, bitmask <<= 1)
            {
                if((b&bitmask) == 0)
                {
                    zero_encountered = true;
                }
                else
                {
                    if(zero_encountered)
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
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

        public bool ValidateCombinedIpNetmask(string s)
        {
            Regex r = new Regex(@"^(((1?\d{1,2})|(2(([0-4]\d)|(5[0-5]))))\.){3}(((1?\d{1,2})|(2(([0-4]\d)|(5[0-5])))))/(([1-2]?[0-9])|(3[0-2]))$");

            return r.IsMatch(s);
        }

        public bool ValidateIpString(string s)
        {
            Regex r = new Regex(@"^(((1?\d{1,2})|(2(([0-4]\d)|(5[0-5]))))\.){3}(((1?\d{1,2})|(2(([0-4]\d)|(5[0-5])))))$");

            return r.IsMatch(s);
        }

        public bool ValidateNetmask(string s)
        {
            Regex r = new Regex(@"^(([1-2]?[0-9])|(3[0-2]))$");

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
