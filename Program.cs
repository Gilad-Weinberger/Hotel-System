using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hotel_System
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] roomDetails = {"ID", "first-name", "last-name", "phone-number", "adults", "kids"};
            string[,] roomsTable = new string[5, roomDetails.Length];
            bool[] roomsStatus = new bool[roomsTable.GetLength(0)];
            int firstVacantRoom = -1, roomToUpdate = -1, orderToDelete = -1;
            string orderID, updateID, deleteID, adminCode, orderType;
            bool IDisExist = false;

            while (true)
            {
                Console.WriteLine("Welcome to the Gilad's Hotel!");
                Console.WriteLine("");
                Thread.Sleep(1500);
                Console.WriteLine("Enter What you want to do:");
                Console.WriteLine("1: Order a room.");
                Console.WriteLine("2: Update an order.");
                Console.WriteLine("3: Delete an order.");
                Console.WriteLine("4: I am the admin, let me see the room status.");
                Console.WriteLine("5: Quit");
                Console.WriteLine("6: Clear the console.");
                Console.WriteLine("");

                orderType = Console.ReadLine();

                if (orderType == "1")
                {
                    for (int i = 0; i < roomsStatus.Length; i++)
                    {
                        if (roomsTable[i, 0] == null)
                        {
                            roomsStatus[i] = false;
                            firstVacantRoom = i;
                            break;
                        }
                        else
                        {
                            roomsStatus[i] = true;
                        }
                    }

                    if (firstVacantRoom != -1)
                    {
                        CreateOrder(roomsTable, roomDetails, roomsStatus, firstVacantRoom);
                        orderID = roomsTable[firstVacantRoom, 0];
                        Console.WriteLine($"Your order id is {orderID}");
                        Console.WriteLine("REMEMBER IT!");
                    }
                    else
                    {
                        Console.WriteLine("There is no vacant rooms for you, sorry...");
                    }
                    Console.WriteLine("");
                    firstVacantRoom = -1;
                }

                else if (orderType == "2")
                {
                    IDisExist = false;
                    Console.WriteLine("What is your order ID?");
                    updateID = Console.ReadLine();

                    for (int i = 0; i < roomsStatus.Length; i++)
                    {
                        if (roomsTable[i, 0] == updateID)
                        {
                            IDisExist = true;
                            roomToUpdate = i;
                        }
                    }

                    if (IDisExist && roomToUpdate != -1)
                    {
                        UpdateOrder(roomsTable, roomDetails, roomsStatus, roomToUpdate);
                    }

                    else
                    {
                        Console.WriteLine($"There is no {updateID} ID in Gilad's Hotel");
                    }
                }

                else if (orderType == "3")
                {
                    IDisExist = false;
                    Console.WriteLine("What is your order ID?");
                    deleteID = Console.ReadLine();

                    for (int i = 0; i < roomsStatus.Length; i++)
                    {
                        if (roomsTable[i, 0] == deleteID)
                        {
                            IDisExist = true;
                            orderToDelete = i;
                        }
                    }

                    if (IDisExist && roomToUpdate != -1)
                    {
                        Console.WriteLine("Are you sure you want to delete you order? (y / anything else)");
                        string sureToDelete = Console.ReadLine();
                        if (sureToDelete == "y")
                        {
                            DeleteOrder(roomsTable, orderToDelete);
                            Thread.Sleep(2000);
                            Console.WriteLine($"Your {deleteID} order has been deleted.");
                        }
                        else
                        {
                            Console.WriteLine("We will not delete your order.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"There is no {deleteID} ID in Gilad's Hotel");
                    }
                }

                else if (orderType == "4")
                {
                    Console.WriteLine("Enter the admin code:");
                    adminCode = Console.ReadLine();
                    if (adminCode == "admin")
                    {
                        for (int row = 0; row < roomsTable.GetLength(0); row++)
                        {
                            for (int col = 0; col < roomsTable.GetLength(1); col++)
                            {
                                if (roomsTable[row, col] != null)
                                {
                                    Console.Write(roomsTable[row, col] + "\t");
                                }
                                else
                                {
                                    Console.WriteLine("null" + "\t");
                                }
                            }
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("You aren't the admin.");
                    }
                }

                else if (orderType == "5")
                {
                    Console.WriteLine("GoodBye, Gilad's Hotel.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;
                }

                else if (orderType == "6")
                {
                    Console.Clear();
                }

                else
                {
                    Console.WriteLine("This is not an option");
                }
            }
        }

        public static void CreateOrder(string[,] table, string[] details, bool[] status, int roomIndex)
        {
            Random rnd = new Random();
            string ID = rnd.Next(10000, 99999).ToString();
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter number of adults: ");
            string adults = Console.ReadLine();
            Console.Write("Enter number of kids: ");
            string kids = Console.ReadLine();
            Console.WriteLine("");

            while (true)
            {
                if (phoneNumber.Length != 10)
                {
                    Console.Write("Enter A REAL Phone Number: ");
                    phoneNumber = Console.ReadLine();
                }
                else
                    break;

                for (int d = 0; d < status.Length; d++)
                {
                    if (ID == table[d, 0])
                    {
                        ID = rnd.Next(1000, 9999).ToString();
                    }
                }
            }

            string[] newDetails = { ID, firstName, lastName, phoneNumber, adults, kids};

            Console.WriteLine("");

            for (int i = 0; i < newDetails.Length; i++)
            {
                table[roomIndex, i] = newDetails[i];
            }
        }

        public static void UpdateOrder(string[,] table, string[] details, bool[] status, int updateIndex)
        {
            Console.WriteLine("for each detail, enter 'same' if you don't want to change it.");

            for (int j = 1; j < details.Length; j++)
            {
                Console.WriteLine($"Enter a new {details[j]}: ");
                string newDetail = Console.ReadLine();
                if (newDetail != "same")
                {
                    table[updateIndex, j] = newDetail;
                }
            }
        }

        public static void DeleteOrder(string[,] table, int deleteIndex)
        {
            for (int i = 0; i < table.GetLength(1); i++)
            {
                table[deleteIndex, i] = null;
            }
        }
    }
}
