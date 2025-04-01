namespace WinFormsApp26
{
    public partial class Form1 : Form
    {

        const int HorseCount = 5;
        ProgressBar[] progressBars = new ProgressBar[HorseCount];
        Thread[] threads = new Thread[HorseCount];
        int[] results = new int[HorseCount];
        static object locker = new();
        int finishedCount = 0;
        int place = 1;



        public Form1()
        {
            InitializeComponent();

            progressBars[0] = progressBar1;
            progressBars[1] = progressBar2;
            progressBars[2] = progressBar3;
            progressBars[3] = progressBar4;
            progressBars[4] = progressBar5;

        }

        private void RunHorse(int index)
        {
            Random rnd = new Random(index * DateTime.Now.Millisecond);

            while (true)
            {
                Thread.Sleep(rnd.Next(50, 150));
                int step = rnd.Next(1, 5);

                Invoke(new Action(() =>
                {

                    if (progressBars[index].Value + step < progressBars[index].Maximum)
                        progressBars[index].Value += step;
                    else
                        progressBars[index].Value = progressBars[index].Maximum;
                }));

                if (progressBars[index].Value >= 100)
                {


                    lock (locker)
                    {

                        results[index] = place++;
                        finishedCount++;
                        if (finishedCount == HorseCount)
                            ShowResults();
                    }
                    break;


                }
            }
        }



        private void ShowResults()
        {
            string msg = "Результаты гонки:\n\n";
            for (int i = 1; i <= HorseCount; i++)
            {
                for (int j = 0; j < HorseCount; j++)
                {

                    if (results[j] == i)
                        msg += $"{i}-е место: Лошадь #{j + 1}\n";
                }
            }

            Invoke(new Action(() => MessageBox.Show(msg, "Финиш!")));
        }
        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void progressBar4_Click(object sender, EventArgs e)
        {

        }

        private void progressBar5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            finishedCount = 0;
            place = 1;
            Array.Clear(results, 0, results.Length);

            for (int i = 0; i < HorseCount; i++)
            {
                progressBars[i].Value = 0;


                int horseIndex = i;
                threads[i] = new Thread(() => RunHorse(horseIndex));
                threads[i].IsBackground = true;
                threads[i].Start();
            }
        }
    }
}
