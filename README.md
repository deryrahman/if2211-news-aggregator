# News Aggregator

 String matching using Brute Force, KMP, and Boyer Moore Algorithm, from IF2211: Algorithmic Strategy

## Penggunaan
Cara pakai sementara :
- Buka file .sln yang ada di folder NewsAggregator pakai Visual Studio
- Run dari Visual Studio
- POST ke http://localhost:xxxx/api/search dengan kolom id (int, 0 = KMP, 1 = Boyer Moore, 2 = Regex) dan pattern (string, pattern yang mau dicari)
- Nanti bakal return JSON yang berisi daftar berita yang mengandung pattern. Nggak case sensitive.

## Keluaran JSON
Ada dua kemungkinan, yaitu ada error dan gak ada error. Kalau ada error, JSON bakal berbentuk :
```json
{
	"status" : false,
	"data" : "Ini pesan error"
}
```

Kalau gak ada error, JSON bakal berbentuk :
```json
{
	"status" : true,
	"data" : [
		{ Url : "Url Berita", Title : "Judul Berita", Match : "Konten yang sesuai dengan yang dicari"},
		{ Url : "Url Berita", Title : "Judul Berita", Match : "Konten yang sesuai dengan yang dicari"},
		{ Url : "Url Berita", Title : "Judul Berita", Match : "Konten yang sesuai dengan yang dicari"},
		...,
		...,
		{ Url : "Url Berita", Title : "Judul Berita", Match : "Konten yang sesuai dengan yang dicari}
	]
}
```
