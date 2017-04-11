# News Aggregator

 String matching using Brute Force, KMP, and Boyer Moore Algorithm, from IF2211: Algorithmic Strategy

## Penggunaan
Cara pakai sementara :
- Buka file .sln yang ada di folder NewsAggregator pakai Visual Studio
- Run dari Visual Studio
- POST ke http://localhost:xxxx/api/search dengan kolom id (int, belum berguna) dan pattern (string, pattern yang mau dicari)
- Nanti bakal return JSON yang berisi daftar berita yang mengandung pattern. Nggak case sensitive.
