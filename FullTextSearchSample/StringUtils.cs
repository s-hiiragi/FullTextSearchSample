using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace FullTextSeachSample
{
    /// <summary>
    /// 汎用に使える構文エラー例外クラス
    /// </summary>
    public class SyntaxErrorException : Exception
    {
        public SyntaxErrorException() { }
        public SyntaxErrorException(string message) : base(message) { }
        public SyntaxErrorException(string message, Exception inner) : base(message, inner) { }
    }
    
    public class StringUtils
    {
        public static readonly ReadOnlyCollection<string> EmptyCollection = 
            new ReadOnlyCollection<string>(new string[0]);

        /// <summary>
        /// 文字コードを判別する
        /// </summary>
        /// 
        /// <param name="bytes">文字コードを調べるデータ</param>
        /// <returns>適当と思われるEncodingオブジェクト。
        /// 判断できなかった時はnull。</returns>
        /// 
        /// <remarks>
        /// Jcode.pmのgetcodeメソッドを移植したものです。
        /// Jcode.pm(http://openlab.ring.gr.jp/Jcode/index-j.html)
        /// Jcode.pmのCopyright: Copyright 1999-2005 Dan Kogai
        /// 
        /// 引用元
        /// http://dobon.net/vb/dotnet/string/detectcode.html
        /// </remarks>
        public static Encoding GetEncoding(byte[] bytes)
        {
            const byte bEscape = 0x1B;
            const byte bAt = 0x40;
            const byte bDollar = 0x24;
            const byte bAnd = 0x26;
            const byte bOpen = 0x28;    //'('
            const byte bB = 0x42;
            const byte bD = 0x44;
            const byte bJ = 0x4A;
            const byte bI = 0x49;

            int len = bytes.Length;
            byte b1, b2, b3, b4;

            //Encode::is_utf8 は無視

            bool isBinary = false;
            for (int i = 0; i < len; i++)
            {
                b1 = bytes[i];
                if (b1 <= 0x06 || b1 == 0x7F || b1 == 0xFF)
                {
                    //'binary'
                    isBinary = true;
                    if (b1 == 0x00 && i < len - 1 && bytes[i + 1] <= 0x7F)
                    {
                        //smells like raw unicode
                        return System.Text.Encoding.Unicode;
                    }
                }
            }
            if (isBinary)
            {
                return null;
            }

            //not Japanese
            bool notJapanese = true;
            for (int i = 0; i < len; i++)
            {
                b1 = bytes[i];
                if (b1 == bEscape || 0x80 <= b1)
                {
                    notJapanese = false;
                    break;
                }
            }
            if (notJapanese)
            {
                return System.Text.Encoding.ASCII;
            }

            for (int i = 0; i < len - 2; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                b3 = bytes[i + 2];

                if (b1 == bEscape)
                {
                    if (b2 == bDollar && b3 == bAt)
                    {
                        //JIS_0208 1978
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    else if (b2 == bDollar && b3 == bB)
                    {
                        //JIS_0208 1983
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    else if (b2 == bOpen && (b3 == bB || b3 == bJ))
                    {
                        //JIS_ASC
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    else if (b2 == bOpen && b3 == bI)
                    {
                        //JIS_KANA
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    if (i < len - 3)
                    {
                        b4 = bytes[i + 3];
                        if (b2 == bDollar && b3 == bOpen && b4 == bD)
                        {
                            //JIS_0212
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        }
                        if (i < len - 5 &&
                            b2 == bAnd && b3 == bAt && b4 == bEscape &&
                            bytes[i + 4] == bDollar && bytes[i + 5] == bB)
                        {
                            //JIS_0208 1990
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        }
                    }
                }
            }

            //should be euc|sjis|utf8
            //use of (?:) by Hiroki Ohzaki <ohzaki@iod.ricoh.co.jp>
            int sjis = 0;
            int euc = 0;
            int utf8 = 0;
            for (int i = 0; i < len - 1; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                if (((0x81 <= b1 && b1 <= 0x9F) || (0xE0 <= b1 && b1 <= 0xFC)) &&
                    ((0x40 <= b2 && b2 <= 0x7E) || (0x80 <= b2 && b2 <= 0xFC)))
                {
                    //SJIS_C
                    sjis += 2;
                    i++;
                }
            }
            for (int i = 0; i < len - 1; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                if (((0xA1 <= b1 && b1 <= 0xFE) && (0xA1 <= b2 && b2 <= 0xFE)) ||
                    (b1 == 0x8E && (0xA1 <= b2 && b2 <= 0xDF)))
                {
                    //EUC_C
                    //EUC_KANA
                    euc += 2;
                    i++;
                }
                else if (i < len - 2)
                {
                    b3 = bytes[i + 2];
                    if (b1 == 0x8F && (0xA1 <= b2 && b2 <= 0xFE) &&
                        (0xA1 <= b3 && b3 <= 0xFE))
                    {
                        //EUC_0212
                        euc += 3;
                        i += 2;
                    }
                }
            }
            for (int i = 0; i < len - 1; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                if ((0xC0 <= b1 && b1 <= 0xDF) && (0x80 <= b2 && b2 <= 0xBF))
                {
                    //UTF8
                    utf8 += 2;
                    i++;
                }
                else if (i < len - 2)
                {
                    b3 = bytes[i + 2];
                    if ((0xE0 <= b1 && b1 <= 0xEF) && (0x80 <= b2 && b2 <= 0xBF) &&
                        (0x80 <= b3 && b3 <= 0xBF))
                    {
                        //UTF8
                        utf8 += 3;
                        i += 2;
                    }
                }
            }
            //M. Takahashi's suggestion
            //utf8 += utf8 / 2;

            System.Diagnostics.Debug.WriteLine(
                string.Format("sjis = {0}, euc = {1}, utf8 = {2}", sjis, euc, utf8));
            if (euc > sjis && euc > utf8)
            {
                //EUC
                return System.Text.Encoding.GetEncoding(51932);
            }
            else if (sjis > euc && sjis > utf8)
            {
                //SJIS
                return System.Text.Encoding.GetEncoding(932);
            }
            else if (utf8 > euc && utf8 > sjis)
            {
                //UTF8
                return System.Text.Encoding.UTF8;
            }

            return null;
        }
    }

    /// <summary>
    /// コマンドライン引数をパースするクラス
    /// 機能
    /// ・変数を埋め込むことができる
    /// 　- $変数名
    /// 　- ${変数名} (変数名の後に文字列を続けたい場合)
    /// 　- $$ => $ (文字$を含めたい場合、$$と重ねる必要がある)
    /// </summary>
    /// <remarks>
    /// TODO
    ///   - 以下のメンバを追加する
    ///     - parse(string text, IDictionary＜string, string＞ dynamicVariables)
    ///     - Varidate(string text, IEnumerable＜string＞ varNames)
    /// </remarks>
    public class CommandLineParser
    {
        #region プロパティ

        /// <summary>
        /// 変数名と値のペア
        /// 値がnullの変数はparse時に渡す
        /// </summary>
        public Dictionary<string, string> Variables { get; set; }

        private string getVariable(string varName)
        {
            if (!Variables.ContainsKey(varName))
                throw new SyntaxErrorException("未定義の変数です。 : " + varName);

            return Variables[varName];
        }

        #endregion

        #region コンストラクタ

        public CommandLineParser()
        {
            Variables = new Dictionary<string, string>();
        }

        public CommandLineParser(IDictionary<string, string> variables)
        {
            Variables = new Dictionary<string, string>(variables);
        }

        #endregion

        public string parse(string text)
        {
            var ret = new StringBuilder();

            for (var it = text.GetEnumerator(); it.MoveNext(); )
            {
                char c = it.Current;
                if (c != '$')
                {
                    ret.Append(c);
                }
                else // c == '$'
                {
                    if (!it.MoveNext())
                        throw new SyntaxErrorException("エスケープすべき文字がありません。");

                    char c2 = it.Current;
                    if (c2 == '$') // $$ => $
                    {
                        ret.Append(c);
                    }
                    else if (c2 == '{') // ${変数名} => 対応する値
                    {
                        // '}' まで読み進める
                        var buf = new StringBuilder();
                        while (true)
                        {
                            if (!it.MoveNext())
                                throw new SyntaxErrorException("変数名が'}'で終端していません。 : " + buf.ToString());

                            if (it.Current == '}')
                            {
                                break;
                            }
                            else
                            {
                                buf.Append(it.Current);
                            }
                        }

                        ret.Append(getVariable(buf.ToString().ToLower()));
                    }
                    else // $変数名 => 対応する値
                    {
                        if (c2 == ' ')
                            throw new SyntaxErrorException("変数名の1文字目が空白文字です。");

                        // 空白か末尾まで読み進める
                        var buf = new StringBuilder();
                        buf.Append(c2);

                        while (it.MoveNext())
                        {
                            if (it.Current == ' ')
                            {
                                ret.Append(it.Current);
                                break;
                            }
                            else
                            {
                                buf.Append(it.Current);
                            }
                        }

                        ret.Append(getVariable(buf.ToString().ToLower()));
                    }
                }
            }

            return ret.ToString();
        }

        /// <summary>
        /// コマンドライン引数の構文チェックを行います。
        /// <c>Variables</c>に登録されていない変数名が出現するとエラーにします。
        /// </summary>
        /// <param name="text">コマンドライン引数</param>
        /// <exception cref="SyntaxErrorException">構文エラー。詳細はmessageで確認してください。</exception>
        public void validate(string text)
        {
            Validate(text, s => Variables.ContainsKey(s));
        }

        /// <summary>
        /// コマンドライン引数の構文チェックを行います。
        /// 変数が存在するかどうかに関わらず、構文のみをチェックします。
        /// </summary>
        /// <param name="text">コマンドライン引数</param>
        /// <exception cref="SyntaxErrorException">構文エラー。詳細はmessageで確認してください。</exception>
        public static void Validate(string text)
        {
            Validate(text, s => true);
        }

        /// <summary>
        /// コマンドライン引数の構文チェックを行います。
        /// 変数名のチェックは<c>varNameTester</c>メソッドで行います。
        /// </summary>
        /// <param name="text">コマンドライン引数</param>
        /// <param name="varNameTester">変数名テスト関数。falseを返すと未定義変数エラーをthrowします。</param>
        /// <exception cref="SyntaxErrorException">構文エラー。詳細はmessageで確認してください。</exception>
        public static void Validate(string text, Func<string, Boolean> varNameTester)
        {
            for (var it = text.GetEnumerator(); it.MoveNext(); )
            {
                if (it.Current == '$')
                {
                    if (!it.MoveNext())
                        throw new SyntaxErrorException("エスケープすべき文字がありません。");

                    else if (it.Current == '{') // ${変数名} => 対応する値
                    {
                        // '}' まで読み進める
                        var buf = new StringBuilder();
                        while (true)
                        {
                            if (!it.MoveNext())
                                throw new SyntaxErrorException("変数名が'}'で終端していません。 : " + buf.ToString());

                            if (it.Current == '}')
                                break;
                            else
                                buf.Append(it.Current);
                        }

                        var varName = buf.ToString().ToLower();
                        if (!varNameTester(varName))
                            throw new SyntaxErrorException("未定義の変数です。 : " + varName);
                    }
                    else if (it.Current != '$') // $変数名 => 対応する値
                    {
                        if (it.Current == ' ')
                            throw new SyntaxErrorException("変数名の1文字目が空白文字です。");

                        // 空白か末尾まで読み進める
                        var buf = new StringBuilder();
                        buf.Append(it.Current);

                        while (it.MoveNext() && it.Current != ' ')
                            buf.Append(it.Current);

                        var varName = buf.ToString().ToLower();
                        if (!varNameTester(varName))
                            throw new SyntaxErrorException("未定義の変数です。 : " + varName);
                    }
                }
            }
        }
    }
}
