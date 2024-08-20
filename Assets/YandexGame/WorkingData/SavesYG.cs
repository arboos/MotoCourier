
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 10000;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        public string SelectedCarName;
        public List<string> ownedCars = new List<string>();
        public int energy = 10;
        public string lastShopUpdateTime;
        public List<string> carsInShop = new List<string>();
        public string musicState = "true";
        public bool gotGiftToday;
        public int gems;
        
        
        //Информация аккаунта
        public int AccountLevel = 1;
        public int AccountMoney = 0;
        
        
        public int ExpToNextLevel = 50;
        public int CurrentExp = 0;
        
        public int BattlePass_Level = 1;
        public int BattlePass_ExpToNextLevel = 50;
        public int BattlePass_CurrentExp = 0;
        
        
        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
        
        public bool HasCar(string carName)
        {
            return ownedCars.Contains(carName);
        }

        // Method to add a car to the player's owned cars
        public void AddCar(string carName)
        {
            if (!ownedCars.Contains(carName))
            {
                ownedCars.Add(carName);
            }
        }
    }
}
