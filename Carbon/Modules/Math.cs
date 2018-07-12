using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace Carbon.Modules
{
    [Name("Math")]
    [Summary("Perform mathematic operations")]
    public class Math : ModuleBase<SocketCommandContext>
    {
        [Command("isinteger")]
        [Summary("Check if the input text is a whole number.")]
        public Task IsInteger(int number)
            => ReplyAsync($"The text {number} is a number!");

        [Command("multiply")]
        [Summary("Obtain the products of two numbers")]
        public async Task Say(int a, int b)
        {
            int product = a * b;
            await ReplyAsync($"The product of `{a} * {b}` is `{product}`.");
        }
        [Command("addmany")]
        [Summary("Obtain the sum of many numbers")]
        public async Task Say(params int[] numbers)
        {
            int sum = numbers.Sum();
            await ReplyAsync($"The sum of `{string.Join(", ", numbers)}` is `{sum}");
        }
    }
}
