using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Updater.Properties;
using Updater.UtillsClasses;

namespace Updater.Localization
{
    public class LocalizationsViewModel : ViewModelBase
    {
        public event EventHandler LanguageChanged;

        public IEnumerable<Languages> Languages { get; }

        private Languages _selectedLanguage;

        public Languages SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                LangInfo.Lang = SelectedLanguage;
                OnPropertyChanged(nameof(SelectedLanguage));
                OnLanguageChanged();
            }
        }

        #region Login localization

        public LocString WelcomeText { get; } = new LocString("Добро пожаловать", "Welcome", "환영합니다", "歡迎光臨");

        public LocString RememberAccountText { get; } = new LocString("Запомнить аккаунт", "Remember account", "계정 기억", "記住帳號");

        public LocString LoginText { get; } = new LocString("Войти", "LogIn", "계정에 로그인", "登錄帳戶");

        public LocString RegisterText { get; } = new LocString("Регистрация", "Registration", "등록", "報名");

        public LocString ForgotPassText { get; } = new LocString("Забыли пароль?", "Forgot password?", "비밀번호 복구", "忘記密碼");

        public LocString SiteText { get; } = new LocString("Официальный сайт", "Web site", "공식 웹 사이트", "官方網站");

        public LocString ForumText { get; } = new LocString("Форум", "Forum", "공개 토론회", "座談會");

        public LocString SupportText { get; } = new LocString("Поддержка", "Support", "지원하다", "支持");

        public LocString AuthText { get; } = new LocString("Авторизация аккаунта", "Account authorization", "계정 인증", "帳戶授權");

        public LocString LoginPlaceHolderText { get; } = new LocString("Почта или логин", "Email or login", "Email or login", "Email or login");

        #endregion

        #region Main localization

        #endregion

        public LocalizationsViewModel()
        {
            Languages = Enum.GetValues(typeof(Languages)).Cast<Languages>();
            SelectedLanguage = Languages.FirstOrDefault(c => c == (Languages)Settings.Default.Lang);
        }

        protected virtual void OnLanguageChanged()
        {
            OnPropertyChanged(nameof(WelcomeText));
            OnPropertyChanged(nameof(RememberAccountText));
            OnPropertyChanged(nameof(LoginText));
            OnPropertyChanged(nameof(RegisterText));
            OnPropertyChanged(nameof(ForgotPassText));
            OnPropertyChanged(nameof(SiteText));
            OnPropertyChanged(nameof(ForumText));
            OnPropertyChanged(nameof(SupportText));
            OnPropertyChanged(nameof(AuthText));
            OnPropertyChanged(nameof(LoginPlaceHolderText));

            LanguageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
