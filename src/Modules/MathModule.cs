using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace crackdotnet.Modules
{
    [Name("math")]
    [Summary("we do be doin math")]
    public class MathModule : ModuleBase<SocketCommandContext>
    {
        [Command("isinteger")]
        [Summary("check if the input text is a whole number")]
        public Task IsInteger(int number)
            => ReplyAsync($"the text {number} is a number!");
        
        [Command("multiply")]
        [Summary("what the fuck do you think idiot")]
        public async Task Say(int a, int b)
        {
            int product = a * b;
            await ReplyAsync($"The product of `{a} * {b}` is `{product}`.");
        }

        [Command("addmany")]
        [Summary("get the sum of many numbers")]
        public async Task Say(params int[] numbers)
        {
            int sum = numbers.Sum();
            await ReplyAsync($"The sum of `{string.Join(", ", numbers)}` is `{sum}`.");
        }
    }
}
