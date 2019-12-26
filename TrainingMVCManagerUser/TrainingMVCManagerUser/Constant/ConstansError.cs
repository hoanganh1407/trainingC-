
using System;

namespace TrainingMVCManagerUser.Constant
{
    /// <summary>
    /// chứa các hằng số về message lỗi
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public static class ConstansError
    {
        public const string ERR001_UserName = "「アカウント名」を入力してください";
        public const string ERRR001_Pass = "「パスワード」を入力してください";
        public const string ERRR016 = "「アカウント名」または「パスワード」は不正です。";

        public const string ERR001_LOGIN_NAME = "「アカウント名」を入力してください";
        public const string ERR001_FULL_NAME = "「氏名」を入力してください";
        public const string ERR001_EMAIL = "「メールアドレス」を入力してください";
        public const string ERR001_TEL = "「電話番号」を入力してください";
        public const string ERR001_TOTAL = "「点数」を入力してください";
        public const string ERR001_PASSWORD = "「パスワード」を入力してください";

        public const string ERR002_GROUP = "「グループ」を入力してください";

        public const string ERR003_LOGIN_NAME = "「アカウント名」は既に存在しています。";
        public const string ERR004_GROUP = "「グループ」は存在していません。";
        public const string ERR004_CODE_LEVEL = "「資格」は存在していません。";

        public const string ERR006_FULL_NAME = "255桁以内の「氏名」を入力してください";
        public const string ERR006_EMAIL = "255桁以内の「メールアドレス」を入力してください";
        public const string ERR006_TEL = "14桁以内の「電話番号」を入力してください";
        public const string ERR006_FULL_NAME_KANA = "255桁以内の「カタカナ氏名」を入力してください";

        public const string ERR007_LOGIN_NAME = "「アカウント名」を4＜＝桁数、＜＝15桁で入力してください";
        public const string ERR007_PASSWORD = "「パスワード」を5＜＝桁数、＜＝15桁で入力してください";

        public const string ERR008_PASSWORD = "「パスワード」に半角英数を入力してください";

        public const string ERR009_FULL_NAME_KANA = "「カタカナ氏名」をカタカナで入力してください";

        public const string ERR011_BIRTHDAY = "「生年月日」は無効になっています。";
        public const string ERR011_EMAIL = "「メールアドレス」は無効になっています。";
        public const string ERR011_EMAIL_EXIST = "「メールアドレス」は既に存在しています。";
        public const string ERR011_TEL = "「電話番号」をxxxx-xxxx-xxxx形式で入力してください";
        public const string ERR011_START_DATE = "「資格交付日」は無効になっています。";
        public const string ERR011_END_DATE = "「失効日」は無効になっています。";

        public const string ERR012_END_DATE = "「失効日」は「交付年月日」より未来の日で入力してください。";
        public const string ERR013 = "該当するユーザは存在していません。";

        public const string ERR015 = "システムエラーが発生しました。";
        public const string ERR017_PASSWORDCONFIRM = "「パスワード（確認」が不正です。";

        public const string ERR018_TOTAL = "「点数」は半角で入力してください";
        public const string ERR019_LOGIN_NAME = "[アカウント名]は(a-z, A-Z, 0-9 と _)の桁のみです。最初の桁は数字ではない。";

        public const string MSG001 = "ユーザの登録が完了しました。";
        public const string MSG002 = "ユーザの更新が完了しました。";
        public const string MSG003 = "ユーザの削除が完了しました。";
        public const string MSG004 = "削除しますが、よろしいでしょうか。";
        public const string MSG005 = "検索条件に該当するユーザが見つかりません。";

        public const String ER001_USER_NAME = "「アカウント名」を入力してください";
        public const String ER001_PASSWORD = "「パスワード」を入力してください";
        public const String ER016 = "「アカウント名」または「パスワード」は不正です。";
    }
}