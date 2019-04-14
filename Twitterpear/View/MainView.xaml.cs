using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Twitterpear.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        // Seconds to milliseconds conversion (seconds * 1000)
        const float FadeAnimationTime = 5f * 1000;
        const float ShowAnimationTime = 300f;
        public static List<AnimationSet> animations = new List<AnimationSet>();

        public MainView()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.LoadUser();
        }

        private async void TweetTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            TweetButton.IsEnabled = !string.IsNullOrEmpty(textBox.Text);

            await StartAnimatingTextBox(sender as TextBox);
        }

        private async Task StartAnimatingTextBox(TextBox textBox)
        {
            int count = animations.Count;
            if (count > 0)
            {
                List<AnimationSet> animationsToRemove = new List<AnimationSet>();
                for (int i = 0; i < count; i++)
                {
                    animations[i].Completed -= Fade_Completed;
                    animations[i].Stop();
                    animationsToRemove.Add(animations[i]);
                }
                int deleteCount = animationsToRemove.Count;
                for (int i = 0; i < count; i++)
                {
                    animations.Remove(animationsToRemove[i]);
                }
                animationsToRemove.Clear();
            }


            var changedTextBox = (TextBox)textBox;

            await changedTextBox.Fade(1, 0).StartAsync();

            if (TweetButton.IsEnabled)
            {

                double duration = FadeAnimationTime;
                var fade = TweetTextBox.Fade(0, duration / 2, duration / 2);
                fade.Completed += Fade_Completed;
                animations.Add(fade);
                await fade.StartAsync();
            }

        }


        private async void Fade_Completed(object sender, AnimationSetCompletedEventArgs e)
        {
            TweetTextBox.Text = "";
            await TweetTextBox.Fade(1,ShowAnimationTime).StartAsync();
        }

        private void ProfilePictureImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var profilePic = sender as Ellipse;
            FlyoutBase.ShowAttachedFlyout(profilePic);
        }

        private void ProfilePictureImage_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var profilePic = sender as Ellipse;
            profilePic.StrokeThickness = 4;
        }

        private void ProfilePictureImage_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            var profilePic = sender as Ellipse;
            profilePic.StrokeThickness = 0;
        }
    }
}

