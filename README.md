FullTextSearchSample
===================

This app is a kind of a GUI version of grep.

* * *

概要
-----
自作のソフトに全文検索を組み込むために作ったサンプルプログラムです。

機能
-----
* フォルダ内のテキストファイルを全文検索(行単位)できます。
	* サブフォルダも検索されます。
	* 複数のフォルダを指定できます。
	* 検索対象ファイルを拡張子でフィルタリングできます。
* マッチした行をエディタで開けます。
	* ファイル名と行,列番号をエディタに渡しているだけです。
	* `設定 > テキストエディタを登録` メニューからカスタマイズできます。

使い方
-----
1. `FullTextSearchSample.exe` を起動します。
2. `設定 > 検索するディレクトリを登録` で検索したいフォルダを登録します。
3. 検索ボックスにキーワードを入れて `Enter` or `検索ボタン` を押します。
4. 検索結果の行を `ダブルクリック` することでエディタで開きます。

エディタのコマンドライン引数の設定例
-----
* サクラエディタ

    -Y=${row} -VY=${row}

* K2Editor

    /j${row} ${file}

* Vim (gVim.exe)

    +${row} ${file}

今後の予定
-----
* grep型全文検索は時間がかかるので、インデックス

配布元
-----
[s-hiiragi/FullTextSearchSample](https://github.com/s-hiiragi/FullTextSearchSample)

/ FullTextSearchSample / bin / Release に exeファイルが置いてあります。

ライセンス
-----
Copyright &copy; 2012 s_hiiragi &lt; [http://github.com/s-hiiragi][my_github] &gt;

  [my_github]: http://github.com/s-hiiragi

PerlのJcode.pmをC#向けに移植したコードを使わせてもらっているので、
Artistic Licenseで配布する予定です。

TODO : 後でしっかりと書きなおす
