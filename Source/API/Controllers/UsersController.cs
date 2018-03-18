using System;
using System.Linq;
using System.Web.Http;

namespace API.Controllers
{
    public class UsersController : ApiController
    {
        private HeatingDbEntities dbContext = new HeatingDbEntities();



        [ActionName("AddNewUserAndAssignToBoard")]
        [HttpGet]
        public int AddNewUserAndAssignTobBoard(string username, string password, int boardId)
        {
            // Verificam ca exist un board care are id egaul cu valoarea parametrului boardId
            var board = dbContext.Boards.FirstOrDefault(_ => _.Id.Equals(boardId));
            if (board != null)
                return 0; // Pentru ca nu exista nici-un board atunci se returneaza 0, ceea ce inseamna ca nu sa adaugat userul.

            // Se verifica ca nu exista nici-un user cu acelasi nume
            if (dbContext.Users.FirstOrDefault(_ => _.UserName.Equals(username)) != null)
            {
                return 0;
            }

            // Verificam dava boardul nu este asociat altui user.
            if (dbContext.UserToBoards.FirstOrDefault(_ => _.IdBoard.Equals(board.Id)) != null)
            {
                return 0;
            }

            using (var userTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    User newUser = new User
                    {
                        UserName = username,
                        Password = password,
                        FirstName = "",
                        LastName = ""
                    };

                    dbContext.Users.Add(newUser);
                    dbContext.UserToBoards.Add(new UserToBoard { User = newUser, Board = board });
                    dbContext.SaveChanges();
                    userTransaction.Commit();
                }
                catch (Exception)
                {
                    userTransaction.Rollback();
                    return 0;
                }
            }

            return 1;
        }

        public int AssiciateUserToBoard(int userId, int boardId)
        {
            // Verificam ca exist boardul
            var board = dbContext.Boards.FirstOrDefault(_ => _.Id.Equals(boardId));
            if (board != null)
                return 0;

            // Verificam ca exist userl
            var user = dbContext.Users.FirstOrDefault(_ => _.Id.Equals(userId));
            if (user != null)
                return 0;

            // Verificam dava boardul nu este asociat altui user.
            if (dbContext.UserToBoards.FirstOrDefault(_ => _.IdBoard.Equals(board.Id)) != null)
            {
                return 0;
            }

            dbContext.UserToBoards.Add(new UserToBoard { IdUser = user.Id, IdBoard = board.Id });
            dbContext.SaveChanges();

            return 1;
        }

    }
}
