using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Updater.Annotations;
using Updater.Localization;
using Updater.Properties;
using Updater.UtillsClasses;

namespace Updater
{
    public partial class MainWindow
    {
       

        public LocalizationsViewModel Localizations { get; } = new LocalizationsViewModel();

        private LocString _startText = new LocString("Готов к работе", "Ready to work", "연주 준비", "準備玩");
        private LocString _initText = new LocString("Инициализация", "Initialization", "초기화", "初始化");
        private LocString _cancelText = new LocString("Отменено пользователем", "Canceled by user", "사용자가 취소", "已被用戶取消");
        private LocString _getUpdateInfo = new LocString("Получение информации о обновлении", "Get update information", "업데이트 정보 얻기", "獲取更新信息");
        private LocString _createFolders = new LocString("Создание папок", "Creating folders", "폴더 만들기", "創建文件夾");
        private LocString _updateSuccess = new LocString("Обновление успешно завершено", "Update success", "업데이트가 성공적으로 완료되었습니다", "更新成功完成");
        private LocString _updateError = new LocString("Во время обновления произошла ошибка", "Update error", "업데이트하는 동안 오류가 발생했습니다", "更新時發生錯誤");
        private LocString _checkFiles = new LocString("Проверка файлов для обновления", "Check files for update", "업데이트 할 파일 확인", "檢查文件以進行更新");
        private LocString _selfUpdateStr = new LocString("Доступна новая версия лаунчера", "New version of updater is available", "새로운 버전의 업데이트 사용 가능", "有新版本的更新可用");

        private void LocalizationsOnLanguageChanged(object sender, EventArgs e)
        {
            SetLocalization();
        }

        public void SetLocalization()
        {
            OnPropertyChanged(nameof(Info));
            switch (Localizations.SelectedLanguage)
            {
                case Languages.Rus:
                    SetRus();
                    break;
                case Languages.Eng:
                    SetEng();
                    break;
                case Languages.Kor:
                    SetKor();
                    break;
                case Languages.Chi:
                    SetChi();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetRus()
        {
            Cancel.Content = "Отмена";
            StartButton.Content = "Начать игру";
            FullCheck.Content = "Обновить";
            QuickCheck.Content = "Быстрая проверка";

            Site.Content = "Главная";

            Support.Content = "Тех.поддержка";

            DonateButton.Content = "Пополнить";



        }

        private void SetEng()
        {
            Cancel.Content = "Cancel";
            StartButton.Content = "START GAME";
            FullCheck.Content = "Update files";
            QuickCheck.Content = "QUICK CHECK";

            Site.Content = "Site";

            Support.Content = "Supports";

      
            DonateButton.Content = "Donate";




        }

        private void SetKor()
        {
            Cancel.Content = "취소";
            StartButton.Content = "게임을 시작하다";
            FullCheck.Content = "파일 업데이트";
            QuickCheck.Content = "빠른 확인";

            Site.Content = "웹 사이트";

            Support.Content = "지원하다";

            DonateButton.Content = "기부";

     


        }

        private void SetChi()
        {
            Cancel.Content = "取消";
            StartButton.Content = "开始游戏";
            FullCheck.Content = "更新档案";
            QuickCheck.Content = "快速检查";

            Site.Content = "网站";

            Support.Content = "游戏支持";

            
            DonateButton.Content = "充值余额";


        }
    }
}
