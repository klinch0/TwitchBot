using System.Media;

namespace KunQa_Twith
{
    public partial class Form1 : Form
    {
        public Form1(string path)
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("./pic.jpg");


            SoundPlayer player = new SoundPlayer();

            player.SoundLocation = "./scream.wav";
            player.Play();

            OnStart();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private async Task OnStart()
        {
            await Task.Delay(10000);

            this.Close();
        }
    }
}