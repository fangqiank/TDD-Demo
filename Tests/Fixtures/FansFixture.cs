using Api.Models;

namespace Tests.Fixtures
{
    public class FansFixture
    {
        public static List<Fan> GetFans() => new()
        {
            new Fan
            {
                Id = 1,
                Name = "zhangsan",
                Email = "zhangsan@mail.com"
            },

            new Fan
            {
                Id = 4,
                Name = "lisi",
                Email = "lisi@mail.com"
            },

        };
    }
}
