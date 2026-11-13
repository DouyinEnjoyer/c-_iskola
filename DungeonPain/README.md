# DungeonPain: miért kell az öröklődés és a haladó OOP?

Ez a projekt egy apró, konzolos kazamatajátékot tartalmaz. A játék **működik**, a kódja mégis úgy van megírva, ahogy az ember akkor ír programot, amikor még nem ismeri az öröklődést, a polimorfizmust és az interfészeket: típusjelölő `enum`-ok, hosszú `switch`-ek, szöveges azonosítók, `object` típusú listák, felesleges mezők.

A feladatod három részből áll:

1. **A rész – a fájdalom.** Végigviszel a kódon néhány módosítási kérést (új szörny, új viselkedés, új tárgyak, új szobaelemek, ...), és közben **mérsz**: hány helyen kellett belenyúlnod, mit felejtettél ki, mi volt idegesítő.
2. **B rész – a gyógyír.** Átalakítod a kódot objektumorientálttá úgy, hogy a program kimenete egyetlen számjegyet sem változik.
3. **C rész – az összehasonlítás.** Újabb módosítási kéréseket teljesítesz, most már az átalakított kódon, és összeveted az élményt az A résszel.

A végén biztosan tudni fogod, **mikor érdemes** öröklődést, absztrakt osztályt vagy interfészt használni. Ezt nem tanulni kell, hanem megtapasztalni.

> A konzolos szövegek szándékosan angolul vannak, hogy ne legyen gond az ékezetes karakterek megjelenítésével. A kódban a megjegyzések magyarul vannak.

## Előfeltételek és indítás

- Visual Studio 2022 (a „.NET desktop development” vagy „.NET” munkaterheléssel), .NET 8.
- Nyisd meg a `DungeonPain.sln` fájlt, majd futtasd a programot `Ctrl+F5`-tel (indítás hibakeresés nélkül), hogy a konzolablak a végén nyitva maradjon.
- Ha a konzolablak túl rövid a kimenethez, a kimenet végén lévő **SUMMARY** blokkot akkor is látni fogod, mert az az utolsó néhány sor.

Ajánlott: a kezdőállapotot mentsd el Gitbe (`git init`, `git add .`, `git commit -m "start"`), és minden kör előtt és után commitolj. Ekkor a `git diff --stat` megmutatja, hány fájlt érintett a módosításod, ami a mérőlaphoz jól jön. Git nélkül is megoldható: számold magad.

## Hogyan működik a játék

A program háromszor játssza végig ugyanazt a kazamatát, három különböző hőssel: **Aron** (Warrior), **Bela** (Mage) és **Csilla** (Thief). A kazamata szobákból áll. A hős sorban végigmegy a szobákon, és mindennel foglalkozik, ami bennük van: tárgyat vesz fel, csapdába lép, szörnyekkel harcol. Két szoba között röviden pihen (4 HP-t gyógyul).

A harc körökre osztott: a hős üt, majd (ha a szörny életben maradt) a szörny üt. Minden szám determinisztikus, **nincs véletlen**, ezért a kimenet minden futásnál pontosan ugyanaz. Ez teszi lehetővé, hogy a végeredményt ellenőrizni tudd.

Amit a kezdőállapot tud:

| Hős | Képesség |
| --- | --- |
| Warrior | 50 HP, támadás 7, védelem 3. Ha az élete a felénél kevesebb, +2 sebzést okoz. Csontvázakra +2 sebzést okoz. |
| Mage | 38 HP, támadás 4, védelem 1, 15 mana. Ha van legalább 3 mana, tűzgolyót lő: sebzés = támadás + 4, a védelmet figyelmen kívül hagyja, 3 mana az ára. Különben normál ütés. |
| Thief | 40 HP, támadás 6, védelem 2. Minden második támadása hátbaszúrás: dupla sebzés. |

Normál sebzés: `támadás - védő védelme`, de legalább 1.

| Szörny | HP | Támadás | Védelem | Különlegesség |
| --- | --- | --- | --- | --- |
| Goblin | 12 | 4 | 1 | Minden ütésénél ellop 2 aranyat (ha van a hősnél). Ha legyőzik, visszaadja. |
| Orc | 25 | 6 | 2 | Ha az élete a felére esik, dühbe gurul: támadása tartósan +3. |
| Skeleton | 18 | 5 | 1 | – |
| Slime | 15 | 3 | 0 | Minden ütése után a hős védelme 1-gyel csökken (legalább 0-ig). |
| Dragon | 40 | 11 | 4 | Minden harmadik körében tüzet lehel: fix 8 sebzés, a védelem nem számít. |

A tárgyakat a hős megtalálja vagy zsákmányként megkapja, és **azonnal használja**: `potion` (+10 HP), `sword` (+2 támadás), `shield` (+1 védelem). A csapdák: `spike` (3 sebzés) és `fire` (5 sebzés).

## A projekt felépítése

| Fájl | Mit tartalmaz |
| --- | --- |
| `Program.cs` | A három hős létrehozása, a kazamata végigjátszása, az összegzés. **Az összegzés részét ne írd át!** |
| `Dungeon.cs` | A szobák felépítése és a szobák bejárása. |
| `Combat.cs` | A harc, a hősök támadása és a szörnyek köre. |
| `Monster.cs`, `MonsterFactory.cs`, `MonsterInfo.cs` | A szörnyek adatai, létrehozásuk, jelük, leírásuk, jutalmuk, zsákmányuk. |
| `Hero.cs`, `HeroFactory.cs` | A hősök adatai és létrehozásuk. |
| `Item.cs`, `ItemLogic.cs` | A tárgyak és használatuk. |
| `Trap.cs`, `Room.cs` | A csapdák és a szobák. |

## Az ellenőrzés: az összegzés

A kimenet végén egy **SUMMARY** blokk áll, hősönként egy sorral. Ez a „fix pont”: minden körnek van **elvárt eredménye**, és ha a te összegzésed számról számra megegyezik vele, akkor a feladatot jól oldottad meg. A konzolra kiírt szövegeket (mondatok, formázás) nyugodtan módosíthatod, csak a számoknak kell stimmelniük.

Ha eltérést látsz, nézd át újra a kör szabályait: szinte mindig egy apró részlet maradt ki (egy hős, egy szörnytípus, egy sorrend). A napló (a SUMMARY feletti rész) segít megtalálni, hol romlott el valami.

## A rész: a kód működik, de fájdalmas

### Játékszabályok az A részhez

Az A részben **úgy módosítod a kódot, ahogy az már fel van építve**. Szabad:

- új `enum` értéket, új `case` ágat, új segédfüggvényt írni,
- új mezőt felvenni egy meglévő osztályba,
- logikai (`bool`) vagy szöveges (`string`) jelzőket használni,
- új, egyszerű osztályt létrehozni, ha az nem örököl semmiből.

**Nem szabad** öröklődést, absztrakt osztályt, interfészt vagy `virtual`/`override` metódust használni. Ez nem büntetés, hanem a kísérlet lényege: a fájdalmat csak akkor érzed meg, ha nem kerülöd ki.

### A mérőlap

Minden kör után töltsd ki ezt a táblázatot (másold ki egy külön fájlba vagy füzetbe):

| Kör | Érintett fájlok | Módosított helyek | Idő (perc) | Elsőre kihagytam valamit? | Mi volt a legidegesítőbb? |
| --- | --- | --- | --- | --- | --- |
| 1 |  |  |  |  |  |
| 2 |  |  |  |  |  |
| 3 |  |  |  |  |  |
| 4 |  |  |  |  |  |
| 5 |  |  |  |  |  |

„Módosított hely” alatt egy különálló kódrészletet értünk: egy új `case` ág, egy új függvény, egy új mező mind külön helynek számít.

### 0. kör: ismerkedés és hibakeresés

**1. feladat: térkép.** Olvasd végig a kódot, különösen a `Combat.cs`, `MonsterInfo.cs` és `MonsterFactory.cs` fájlokat. Aztán kattints a `MonsterType.Dragon` értékre, és használd a **Find All References** parancsot (`Shift+F12`). Válaszolj:

- Hány különböző függvényben szerepel a `MonsterType.Dragon`?
- Ha lenne egy hatodik szörnytípus, hány helyre kellene beírnod?
- Mi történik, ha az egyikről megfeledkezel? Ad-e a fordító hibát vagy figyelmeztetést?

**2. feladat: hiba a kódban.** A kezdőállapot kimenete nem teljesen az, aminek lennie kellene. Nézd meg alaposan a naplót: valamelyik zsákmánynál a hős nem azt kapja, amit várnál, és ez a végeredményre is hat. Keresd meg a hibát, javítsd ki, és magyarázd el magadnak, **miért nem szólt a fordító**.

Elvárt eredmény a **javítás előtt** (a kezdőállapot):

```text
Aron   HP 11/50  ATK 11  DEF 4  Gold 5  XP 67  Defeated 5  Fled 0  alive
Bela   HP 0/38  ATK 8  DEF 2  Gold 5  XP 27  Defeated 4  Fled 0  FALLEN
Csilla HP 0/40  ATK 10  DEF 3  Gold 5  XP 27  Defeated 4  Fled 0  FALLEN
```

Elvárt eredmény a **javítás után**:

```text
Aron   HP 21/50  ATK 11  DEF 4  Gold 5  XP 67  Defeated 5  Fled 0  alive
Bela   HP 0/38  ATK 8  DEF 2  Gold 5  XP 27  Defeated 4  Fled 0  FALLEN
Csilla HP 0/40  ATK 10  DEF 3  Gold 5  XP 27  Defeated 4  Fled 0  FALLEN
```

A kezdőállapotban Bela és Csilla elesik. Ez nem hiba, a kazamata nehéz. A további körökben a hősök egyre jobban járnak, mert több tárgyat találnak.

### 1. kör: új szörny, a Troll

A játékosok új ellenfelet szeretnének. **Troll** adatai:

| Név | HP | Támadás | Védelem | Jel | XP | Zsákmány |
| --- | --- | --- | --- | --- | --- | --- |
| Troll | 24 | 5 | 2 | `T` | 20 | shield |

Különlegesség: a Troll a **saját körében**, a hős megütése után **2 HP-t regenerálódik** (legfeljebb a maximális életéig). A leírása szabadon választható.

A kazamatában a Troll a „Troll bridge” szobában áll. A szobát a `Dungeon.cs`-ben egy kommentezett sor jelöli: ha elkészültél, vedd ki a kommentjelet. A típus neve az `enum`-ban legyen `Troll`.

Elvárt eredmény:

```text
Aron   HP 19/50  ATK 11  DEF 5  Gold 5  XP 87  Defeated 6  Fled 0  alive
Bela   HP 0/38  ATK 8  DEF 3  Gold 5  XP 47  Defeated 5  Fled 0  FALLEN
Csilla HP 0/40  ATK 10  DEF 4  Gold 5  XP 47  Defeated 5  Fled 0  FALLEN
```

Töltsd ki a mérőlap 1. sorát!

### 2. kör: új viselkedés, a menekülés

Mostantól a szörnyek **elmenekülhetnek**. Szabályok:

- A hős minden ütése után, ha a szörny még él, ellenőrizd: ha `Hp * 100 / MaxHp` (egész osztással) **kisebb**, mint a szörny menekülési küszöbe, a szörny elmenekül, és a harc véget ér. Ez még azelőtt történik, hogy a szörny ütne.
- A menekülő szörny után a hős **nem kap zsákmányt**, az ellopott aranyat sem kapja vissza, de megkapja a szörny XP-jének **felét** (egész osztással), és a `Fled` számlálója eggyel nő. A `Defeated` ilyenkor nem nő.
- A menekülési küszöbök (százalék):

| Goblin | Orc | Skeleton | Slime | Troll | Dragon |
| --- | --- | --- | --- | --- | --- |
| 30 | 0 | 0 | 0 | 0 | 15 |

A 0 azt jelenti, hogy a szörny soha nem menekül.

Elvárt eredmény:

```text
Aron   HP 15/50  ATK 11  DEF 5  Gold 5  XP 67  Defeated 5  Fled 1  alive
Bela   HP 0/38  ATK 8  DEF 3  Gold 5  XP 44  Defeated 4  Fled 1  FALLEN
Csilla HP 0/40  ATK 10  DEF 4  Gold 5  XP 47  Defeated 5  Fled 0  FALLEN
```

Figyeld meg: itt **nem új típust** vettél fel, hanem egy **új műveletet**, amit minden típusra külön meg kellett határozni. Írd fel a mérőlapra, hogyan érezted magad a két kör között.

### 3. kör: új tárgyak

Két új tárgy érkezik. A tárgyak neve (a `Kind` értéke) legyen pontosan `elixir` és `helmet`.

| Tárgy | Hatás |
| --- | --- |
| `elixir` | +10 HP (legfeljebb a maximumig) és +1 támadás |
| `helmet` | +1 védelem |

Ezen felül a **Slime** mostantól `elixir`-t dob zsákmányként (eddig semmit). A „Storage room” szobát a `Dungeon.cs`-ben kommentezett sor jelöli.

**Kísérlet:** miután minden működik, egyetlen helyen írd át az `elixir` szót `Elixir`-re (nagy kezdőbetű), és futtasd a programot. Kaptál hibát? Figyelmeztetést? Mi a jele annak, hogy valami elromlott? Utána javítsd vissza.

Elvárt eredmény:

```text
Aron   HP 36/50  ATK 13  DEF 6  Gold 5  XP 67  Defeated 5  Fled 1  alive
Bela   HP 6/38  ATK 10  DEF 4  Gold 5  XP 64  Defeated 4  Fled 2  alive
Csilla HP 34/40  ATK 12  DEF 5  Gold 5  XP 87  Defeated 6  Fled 0  alive
```

### 4. kör: új szobaelemek, a Fountain és a Chest

Két új dolog kerül a szobákba. A `Dungeon.cs` kommentezett sorai pontosan megmondják, hogyan kell létrehozni őket: `new Fountain()` és `new Chest(5, new Item("elixir"), new Item("helmet"))`. Ehhez a két osztályt neked kell megírnod, a konstruktorok az említett módon működjenek.

- **Fountain:** amikor a hős belép a szobába, 8 HP-t gyógyul (legfeljebb a maximumig).
- **Chest:** a hős megkapja a benne lévő aranyat (az összeget a `Gold` mezőjéhez adja), majd az összes tárgyat felveszi és azonnal használja, a megadott sorrendben.

**Kísérlet:** először csak a két osztályt hozd létre, vedd ki a kommentjelet a szobákról, és futtasd a programot **anélkül**, hogy a `Dungeon.Run`-ban kezelnéd őket. Mit látsz a kimeneten? Szól-e bármi arról, hogy a szoba tartalmát senki sem dolgozta fel? Csak ezután írd meg a hiányzó részt.

Elvárt eredmény:

```text
Aron   HP 48/50  ATK 14  DEF 7  Gold 10  XP 87  Defeated 6  Fled 0  alive
Bela   HP 16/38  ATK 11  DEF 5  Gold 10  XP 64  Defeated 4  Fled 2  alive
Csilla HP 34/40  ATK 13  DEF 6  Gold 10  XP 67  Defeated 5  Fled 1  alive
```

### 5. kör: repülő és mérgező szörnyek

Három új szörny érkezik, két új tulajdonsággal: **repülés** és **mérgezés**. A Wyvern **mindkettővel** rendelkezik.

| Név | HP | Támadás | Védelem | Jel | XP | Zsákmány | Menekülési küszöb | Repül | Mérgező |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Bat | 10 | 3 | 0 | `b` | 6 | nincs | 40 | igen | nem |
| Spider | 16 | 4 | 1 | `S` | 9 | potion | 0 | nem | igen |
| Wyvern | 30 | 9 | 3 | `W` | 30 | elixir | 0 | igen | igen |

Az `enum` értékek neve legyen `Bat`, `Spider`, `Wyvern`. A szobák a `Dungeon.cs`-ben kommentezve megvannak.

**A repülés szabályai:**

- A **Warrior** sebzése (az összes többi módosító után, a legalább 1 szabály előtt) **felére csökken** egész osztással: közelharcban nehéz eltalálni a repülőt.
- A **Thief** hátbaszúrása repülő ellen **nem dupláz**. A támadásszámláló ettől függetlenül növekszik.
- A **Mage** tűzgolyóját a repülés nem befolyásolja.

**A mérgezés szabályai:**

- Amikor egy mérgező szörny a saját körében megüti a hőst, a hős `PoisonTurns` értéke 3 lesz (ha már mérgezett volt, újra 3). Ehhez a hősnek új mezőre lesz szüksége.
- A harcban a hős **minden támadási köre elején**, ha `PoisonTurns > 0`, a hős 2 HP-t veszít, és `PoisonTurns` eggyel csökken. Ha ettől a hős életereje 0 alá esik, a harc a hős elestével véget ér.
- A harc végén (akár győzelem, akár menekülés) a `PoisonTurns` nullázódik.

Elvárt eredmény:

```text
Aron   HP 42/50  ATK 15  DEF 7  Gold 10  XP 129  Defeated 8  Fled 1  alive
Bela   HP 22/38  ATK 12  DEF 5  Gold 10  XP 129  Defeated 8  Fled 1  alive
Csilla HP 40/40  ATK 14  DEF 6  Gold 10  XP 132  Defeated 9  Fled 0  alive
```

Ez volt a legnagyobb kör. Még mielőtt kitöltöd a mérőlapot, jegyezd fel külön, **hogyan döntöttél**: új `enum` értékekkel oldottad meg, `bool` mezőkkel, vagy valami mással? Hány helyen kellett ugyanazt a „repül-e” vagy „mérgező-e” kérdést feltenned? Hogyan néz ki a kódod a Wyvernnél?

### Megállj, beszéljétek meg!

Mielőtt a B részre léptek, válaszoljatok párban vagy csoportban:

1. Melyik kör volt a legfájdalmasabb, és miért? Ami fájt, az **mennyiben** a szörnyek száma, és **mennyiben** a feladat jellege miatt volt?
2. Hányszor fordult elő, hogy valamit kihagytál, és a **fordító nem szólt**? Mi volt ennek az ára?
3. Képzeld el, hogy a játéknak 40 szörnye, 8 hőse és 30 tárgya van. Mi változna?
4. Mi a közös az 1., a 2. és az 5. körben? (Segítség: nézd meg, **hova** kellett nyúlni a kódban: egy helyre, vagy sok helyre?)

## B rész: átalakítás objektumorientálttá

Most jön a gyógyír. A feladat: alakítsd át a kódot úgy, hogy az A rész végén kapott kimenet **pontosan ugyanaz** maradjon (a SUMMARY számai egyezzenek az 5. kör elvárt eredményével), de a szerkezete objektumorientált legyen.

Szabályok:

- Dolgozz **kis lépésekben**, és minden lépés után futtasd a programot. Ha a SUMMARY eltér az 5. kör eredményétől, akkor az előző lépésben hibáztál, nem kell messzire keresni.
- Ha a Visual Studio vagy a Rider/ReSharper automatikus átalakítást kínál (Quick Actions: `Ctrl+.`), használd, a gépi segítség nem csalás.
- Ha elakadsz, nézd át a tananyag öröklődésről, polimorfizmusról, absztrakt osztályról és interfészről szóló fejezeteit.

A lépések nem kötelező sorrendet adnak, de ebben a sorrendben a legkönnyebb haladni.

### 1. lépés: a szörnyek

**Cél:** eltűnik a `MonsterType` enum és a `MonsterFactory`, a `MonsterInfo` és a `MonsterAct` összes `switch`-e.

Kérdések, amelyek segítenek:

- Mi közös **minden** szörnyben? Mi különbözik? A közös rész és a különbözőség hova kerüljön?
- Melyik `switch` tartalma legyen minden szörnynél **kötelezően** megadandó, és melyiké legyen olyan, aminek van értelmes **alapértelmezése** (például a menekülési küszöb)?
- Hogyan biztosítod, hogy ha új szörnyet adsz hozzá, és kihagysz valamit, azt **a fordító** szóljon, ne a futás során vedd észre?
- Hol lesz a Goblin ellopott aranya, a Dragon körszámlálója, az Orc dühe? Kell még egyáltalán az összes szörnynek ott lógó, használatlan mező?

Haladj szörnyenként. Az `enum`-ot hagyd bent, amíg az utolsó `switch` is el nem tűnik, és csak akkor töröld.

### 2. lépés: a repülés és a mérgezés

Itt jön a rész, amiért az egészet csináltuk. A Bat repül, a Spider mérgező, a Wyvern **mindkettő**.

- Először próbáld ki **csak öröklődéssel**: legyen egy `FlyingMonster` és egy `PoisonousMonster` osztályod. Hová tennéd a Wyvernt? Mit tudsz megoldani egyszeres öröklődéssel, és mi az, ami nem megy?
- Milyen más eszköz létezik arra, hogy egy osztály **képességet** vállaljon, függetlenül attól, hogy honnan öröklődik? (Nézd át a tananyagban az interfészről és az absztrakt osztály és az interfész különbségéről szóló részt.)
- Ki kérdezi meg, hogy egy szörny repül-e? A szörny kérdezi önmagától, vagy a támadó hős? Mit jelent ez a kódban?

### 3. lépés: a hősök

- A hősök `switch`-e a `HeroClass` szerint ágazik el. Mi lehet a közös ős, és mi legyen a leszármazottakban?
- A Mage `Mana` mezője és a Thief `AttackCount` mezője: hol van most a helyük? Hol lenne a helyük az átalakítás után?
- A támadás képlete minden hősnél más. Hogyan lehet elérni, hogy a `Combat` ne tudjon a hős típusáról semmit?

### 4. lépés: a tárgyak és a csapdák

- A tárgyaknál a szöveges `Kind` hordozza a típust. Mit cserélnél le rá, és mi történik a kódban, ha mostantól elgépelsz valamit?
- Mi a különbség a „tárgy leírása” és a „tárgy használata” között? Melyik való az osztályba, melyik kerülhet a hős kódjába?
- Ugyanez a kérdés a csapdákra.

### 5. lépés: a szobák

- A `Room.Contents` lista `object` típusú. Mit jelent ez, és mit zár ki? Milyen típus lehetne a lista elemtípusa úgy, hogy a `Dungeon.Run`-ban eltűnjön az `is` és a típuskényszerítés?
- Milyen közös művelete van minden szobaelemnek (szörny, tárgy, csapda, forrás, láda)?

### 6. lépés: takarítás

Keress rá az egész projektben (`Ctrl+Shift+F`) a következő szavakra: `switch`, ` is `, `(Monster)`, `(Item)`, `Kind`, `Type ==`. Hány maradt? Mindegyikre kérdezd meg: ez a hely olyasmit csinál, amit az objektum maga is elvégezhetne?

### Ellenőrzés

Az 5. kör elvárt eredménye a mérce:

```text
Aron   HP 42/50  ATK 15  DEF 7  Gold 10  XP 129  Defeated 8  Fled 1  alive
Bela   HP 22/38  ATK 12  DEF 5  Gold 10  XP 129  Defeated 8  Fled 1  alive
Csilla HP 40/40  ATK 14  DEF 6  Gold 10  XP 132  Defeated 9  Fled 0  alive
```

Végül töltsd ki a mérőlap B részét: hány perc volt az átalakítás? Mely lépés volt a legnehezebb? Mi lett volna, ha az A rész elején nekiállsz így felépíteni?

## C rész: ugyanaz, mint az A, de másképp

Most jön az igazság pillanata. Az **átalakított** kódon teljesítsd az alábbi két módosítási kérést, és közben vezesd ugyanazt a mérőlapot (érintett fájlok, módosított helyek, idő).

### C1: új szörny, a Ghost

| Név | HP | Támadás | Védelem | Jel | XP | Zsákmány | Menekülési küszöb | Repül |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Ghost | 20 | 5 | 2 | `G` | 15 | nincs | 0 | igen |

Különlegesség: miután a Ghost megütötte a hőst, **élet-szívást** végez: a hősnek okozott sebzés **felével** (egész osztással) gyógyítja magát, legfeljebb a maximális életéig.

Új szoba: „Haunted hall”, benne egy Ghost. A „Wyvern roost” és a „Dragon's lair” közé kerüljön.

Elvárt eredmény:

```text
Aron   HP 43/50  ATK 15  DEF 7  Gold 10  XP 144  Defeated 9  Fled 1  alive
Bela   HP 25/38  ATK 12  DEF 5  Gold 10  XP 144  Defeated 9  Fled 1  alive
Csilla HP 40/40  ATK 14  DEF 6  Gold 10  XP 147  Defeated 10  Fled 0  alive
```

### C2: új hős, a Paladin

| Név | HP | Támadás | Védelem | Kezdő arany |
| --- | --- | --- | --- | --- |
| Paladin | 46 | 6 | 4 | 5 |

A Paladin támadása: `támadás - védelem` (legalább 1), és mivel közelharcos, a repülő ellenfeleken a sebzése **felére csökken**, mint a Warriornál. Minden támadása után **2 HP-t gyógyul** (legfeljebb a maximumig), még mielőtt a szörny köre jönne.

Add hozzá a hőslistához negyedikként, **Dora** néven. A SUMMARY blokkban neki is meg kell jelennie.

Elvárt eredmény:

```text
Aron   HP 43/50  ATK 15  DEF 7  Gold 10  XP 144  Defeated 9  Fled 1  alive
Bela   HP 25/38  ATK 12  DEF 5  Gold 10  XP 144  Defeated 9  Fled 1  alive
Csilla HP 40/40  ATK 14  DEF 6  Gold 10  XP 147  Defeated 10  Fled 0  alive
Dora   HP 46/46  ATK 14  DEF 8  Gold 10  XP 147  Defeated 10  Fled 0  alive
```

Gondolkodj el: a Warrior és a Paladin ugyanúgy kezeli a repülő ellenfeleket. Hova kerüljön ez a szabály, hogy ne kelljen kétszer megírni?

## Összegzés: hogyan ismered fel a helyzetet?

Idézd fel a projekten szerzett élményeidet, és próbálj válaszolni a saját szavaiddal:

1. Milyen **jelekből** vetted észre, hogy a kódon valami nincs rendben? Mit láttál a kódban, ami már az A részben is fájt?
2. Mikor hasznos az öröklődés, és mikor az interfész? Mit jelent ez a Troll, a Bat és a Wyvern példáján?
3. Ha a szakdolgozatodban vagy a saját projektedben egy új funkciót kell megírnod, milyen kérdéseket teszel fel magadnak, mielőtt leírsz egy `enum`-ot vagy egy `switch`-et?

Ha ezeket megválaszoltad, vesd össze a saját listádat az alábbi táblázattal. A táblázat nem szabály, hanem **szaglászó lista**: ha ezek bármelyikét látod a kódodban, érdemes megállni és elgondolkodni.

| Amit a kódban látsz | Mit jelez | Mivel érdemes próbálkozni |
| --- | --- | --- |
| Ugyanaz a `switch` (vagy `if`-lánc) a típusjelölő szerint **több helyen** | Az egyes típusok viselkedése szétszórva él | Polimorfizmus: közös ős vagy interfész, minden típus a saját viselkedését hordozza |
| Egy új típus felvételekor sok helyre kell nyúlni, és a fordító nem szól, ha kihagysz egyet | A „teljesség” ellenőrzése rád van bízva | Absztrakt metódus vagy interfész: a fordító kényszerít |
| `enum` vagy `string` mint típusjelölő, melyből a viselkedés következik | Kézzel készített típusrendszer | Valódi típusok (osztályok) |
| Olyan mezők, amelyek csak bizonyos típusoknál használatosak | A közös osztály túl sokat akar tudni | Leszármazott osztályok, a típusspecifikus mezők a saját osztályukba |
| `List<object>`, `is`, típuskényszerítés `(Valami)x` | A közös képesség nincs kimondva | Közös ős vagy interfész, amin keresztül a művelet hívható |
| A kód két helyen szinte ugyanazt csinálja | Közös rész nincs kiemelve | Közös ős vagy segédosztály, vagy kompozíció |
| Két képesség keveredik („repül ÉS mérgező”), és az egyszeres öröklődés nem elég | A képességek keresztezik egymást | Interfészek, vagy kompozíció (a képesség egy külön objektum) |
| Szöveges azonosítók (`"potion"`), amelyek elgépelve csendben hibáznak | A fordító nem látja a hibát | Típusok, felsorolás vagy osztályok a szövegek helyett |

És egy kis ellenpélda a végére: ha a programodban **csak két** típus van, amik **soha nem fognak** változni, és csak **egy** helyen ágazol el rájuk, akkor egy `switch` teljesen rendben van. Az OOP eszközei akkor érnek valamit, ha **változás** várható, és ezen a ponton kell eldönteni, hogy megéri-e.
