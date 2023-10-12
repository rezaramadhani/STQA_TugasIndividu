using System;

namespace Activity5
{
    class Node
    {
        // Kelas Node mewakili simpul dalam daftar berantai berganda.
        // Ini terdiri dari bagian informasi dan tautan ke simpul berikut dan sebelumnya.
        public int rollNumber;  // Nomor pendaftaran mahasiswa.
        public string name;     // Nama mahasiswa.
        public Node next;       // Menunjuk ke simpul berikutnya.
        public Node prev;       // Menunjuk ke simpul sebelumnya.
    }

    class DoubleLinkedList
    {
        Node START;

        public DoubleLinkedList()
        {
            START = null; // Inisialisasi simpul awal ke null saat objek DoubleLinkedList dibuat.
        }

        public void addNode()
        {
            int rollNo;   // Variabel untuk menyimpan nomor pendaftaran.
            string nm;    // Variabel untuk menyimpan nama.

            Console.Write("\nMasukkan nomor pendaftaran mahasiswa: ");
            rollNo = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nMasukkan nama mahasiswa: ");
            nm = Console.ReadLine();

            Node newnode = new Node();
            newnode.rollNumber = rollNo;
            newnode.name = nm;

            if (START == null || rollNo <= START.rollNumber)
            {
                if ((START != null) && (rollNo == START.rollNumber))
                {
                    Console.WriteLine("\nNomor pendaftaran ganda tidak diperbolehkan");
                    return;
                }
                newnode.next = START;
                if (START != null)
                    START.prev = newnode;
                newnode.prev = null;
                START = newnode;
                return;
            }

            Node previous, current;
            for (current = previous = START; current != null &&
                rollNo >= current.rollNumber; previous = current, current = current.next)
            {
                if (rollNo == current.rollNumber)
                {
                    Console.WriteLine("\nNomor pendaftaran ganda tidak diperbolehkan");
                    return;
                }
            }
            newnode.next = current;
            newnode.prev = previous;

            if (current == null)
            {
                newnode.next = null;
                previous.next = newnode;
                return;
            }
            current.prev = newnode;
            previous.next = newnode;
        }

        public bool Search(int rollNo, ref Node previous, ref Node current)
        {
            for (previous = current = START; current != null &&
                rollNo != current.rollNumber; previous = current,
                current = current.next)
            { }
            return (current != null);
        }

        public bool delNode(int rollNo)
        {
            Node previous, current;
            previous = current = null;
            if (Search(rollNo, ref previous, ref current) == false)
                return false;

            if (current == START)
            {
                START = START.next;
                if (START != null)
                    START.prev = null;
                return true;
            }

            if (current.next == null)
            {
                previous.next = null;
                return true;
            }

            previous.next = current.next;
            current.next.prev = previous;
            return true;
        }

        public void travarse()
        {
            if (listEmpty())
                Console.WriteLine("\nDaftar kosong");
            else
            {
                Console.WriteLine("\nCatatan dalam urutan naik " + "nomor pendaftaran adalah:\n");
                Node currentNode;
                for (currentNode = START; currentNode != null; currentNode = currentNode.next)
                    Console.Write(currentNode.rollNumber + " " + currentNode.name + "\n");
            }
        }

        public void revtravarse()
        {
            if (listEmpty())
                Console.WriteLine("\nDaftar kosong");
            else
            {
                Console.WriteLine("\nCatatan dalam urutan turun " + "nomor pendaftaran adalah:\n");
                Node currentNode;
                for (currentNode = START; currentNode.next != null;
                    currentNode = currentNode.next)
                { }
                while (currentNode != null)
                {
                    Console.Write(currentNode.rollNumber + " " + currentNode.name + "\n");
                    currentNode = currentNode.prev;
                }
            }
        }

        public bool listEmpty()
        {
            if (START == null)
                return true;
            else
                return false;
        }

        static void Main(string[] args)
        {
            DoubleLinkedList obj = new DoubleLinkedList();
            while (true)
            {
                try
                {
                    Console.WriteLine("\n Menu" +
                        "\n 1. Tambahkan catatan ke daftar" +
                        "\n 2. Hapus catatan dari daftar" +
                        "\n 3. Lihat semua catatan dalam urutan naik berdasarkan nomor pendaftaran" +
                        "\n 4. Lihat semua catatan dalam urutan turun berdasarkan nomor pendaftaran" +
                        "\n 5. Cari catatan dalam daftar" +
                        "\n 6. Keluar \n" +
                        "\n Masukkan pilihan Anda (1-6): ");
                    char ch = Convert.ToChar(Console.ReadLine());
                    switch (ch)
                    {
                        case '1':
                            {
                                obj.addNode();
                            }
                            break;
                        case '2':
                            {
                                if (obj.listEmpty())
                                {
                                    Console.WriteLine("\nDaftar kosong");
                                    break;
                                }
                                Console.Write("\nMasukkan nomor pendaftaran mahasiswa" + "yang catatannya akan dihapus: ");
                                int rollNo = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine();
                                if (obj.delNode(rollNo) == false)
                                    Console.WriteLine("Catatan tidak ditemukan");
                                else
                                    Console.WriteLine("Catatan dengan nomor pendaftaran " + rollNo + "dihapus \n");
                            }
                            break;
                        case '3':
                            {
                                obj.travarse();
                            }
                            break;
                        case '4':
                            {
                                obj.revtravarse();
                            }
                            break;
                        case '5':
                            {
                                if (obj.listEmpty() == true)
                                {
                                    Console.WriteLine("\nDaftar kosong");
                                    break;
                                }
                                Node prev, curr;
                                prev = curr = null;
                                Console.Write("\nMasukkan nomor pendaftaran mahasiswa yang catatannya ingin Anda cari: ");
                                int num = Convert.ToInt32(Console.ReadLine());
                                if (obj.Search(num, ref prev, ref curr) == false)
                                    Console.WriteLine("\nCatatan tidak ditemukan");
                                else
                                {
                                    Console.WriteLine("\nCatatan ditemukan");
                                    Console.WriteLine("\nNomor pendaftaran: " + curr.rollNumber);
                                    Console.WriteLine("\nNama " + curr.name);
                                }
                            }
                            break;
                        case '6':
                            return;
                        default:
                            {
                                Console.WriteLine("\nPilihan tidak valid");
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("check fot the values entered");

                    }
            }
            }
        }
    }