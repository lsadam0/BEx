using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEx
{
    public enum Currency
    {

        // Crypto
        None,
        BTC, // BitCoin
        LTC, // LiteCoin
        NMC, // NameCoin
        NVC, // NovaCoin
        PPC, // PeerCoin
        TRC, // TerraCoin
        FTC, // FeatherCoin
        XPM, // PrimeCoin
        USD, // United States Dollar
        GBP, // Great British Pound
        RUR, // Russian 
        CNH, // Yuan
        EUR, // Euro
        DRK, // DarkCoin
        AED, //	United Arab Emirates Dirham
        AFN, //	Afghanistan Afghani
        ALL, //	Albania Lek
        AMD, //	Armenia Dram
        ANG, //	Netherlands Antilles Guilder
        AOA, //	Angola Kwanza
        ARS, //	Argentina Peso
        AUD, //	Australia Dollar
        AWG, //	Aruba Guilder
        AZN, //	Azerbaijan New Manat
        BAM, //	Bosnia and Herzegovina Convertible Marka
        BBD, //	Barbados Dollar
        BDT, //	Bangladesh Taka
        BGN, //	Bulgaria Lev
        BHD, //	Bahrain Dinar
        BIF, //	Burundi Franc
        BMD, //	Bermuda Dollar
        BND, //	Brunei Darussalam Dollar
        BOB, //	Bolivia Boliviano
        BRL, //	Brazil Real
        BSD, //	Bahamas Dollar
        BTN, //	Bhutan Ngultrum
        BWP, //	Botswana Pula
        BYR, //	Belarus Ruble
        BZD, //	Belize Dollar
        CAD, //	Canada Dollar
        CDF, //	Congo/Kinshasa Franc
        CHF, //	Switzerland Franc
        CLP, //	Chile Peso
        CNY, //	China Yuan Renminbi
        COP, //	Colombia Peso
        CRC, //	Costa Rica Colon    
        CUC, //	Cuba Convertible Peso
        CUP, //	Cuba Peso
        CVE, //	Cape Verde Escudo
        CZK, //	Czech Republic Koruna
        DJF, //	Djibouti Franc
        DKK, //	Denmark Krone
        DOP, //	Dominican Republic Peso
        DZD, //	Algeria Dinar
        EGP, //	Egypt Pound
        ERN, //	Eritrea Nakfa
        ETB, //	Ethiopia Birr
        FJD, //	Fiji Dollar
        FKP, //	Falkland Islands (Malvinas) Pound
        GEL, //	Georgia Lari
        GGP, //	Guernsey Pound
        GHS, //	Ghana Cedi
        GIP, //	Gibraltar Pound
        GMD, //	Gambia Dalasi
        GNF, //	Guinea Franc
        GTQ, //	Guatemala Quetzal
        GYD, //	Guyana Dollar
        HKD, //	Hong Kong Dollar
        HNL, //	Honduras Lempira
        HRK, //	Croatia Kuna
        HTG, //	Haiti Gourde
        HUF, //	Hungary Forint
        IDR, //	Indonesia Rupiah
        ILS, //	Israel Shekel
        IMP, //	Isle of Man Pound
        INR, //	India Rupee
        IQD, //	Iraq Dinar
        IRR, //	Iran Rial
        ISK, //	Iceland Krona
        JEP, //	Jersey Pound
        JMD, //	Jamaica Dollar
        JOD, //	Jordan Dinar
        JPY, //	Japan Yen
        KES, //	Kenya Shilling
        KGS, //	Kyrgyzstan Som
        KHR, //	Cambodia Riel
        KMF, //	Comoros Franc
        KPW, //	Korea (North) Won
        KRW, //	Korea (South) Won
        KWD, //	Kuwait Dinar
        KYD, //	Cayman Islands Dollar
        KZT, //	Kazakhstan Tenge
        LAK, //	Laos Kip
        LBP, //	Lebanon Pound
        LKR, //	Sri Lanka Rupee
        LRD, //	Liberia Dollar
        LSL, //	Lesotho Loti
        LTL, //	Lithuania Litas
        LYD, //	Libya Dinar
        MAD, //	Morocco Dirham
        MDL, //	Moldova Leu
        MGA, //	Madagascar Ariary
        MKD, //	Macedonia Denar
        MMK, //	Myanmar (Burma) Kyat
        MNT, //	Mongolia Tughrik
        MOP, //	Macau Pataca
        MRO, //	Mauritania Ouguiya
        MUR, //	Mauritius Rupee
        MVR, //	Maldives (Maldive Islands) Rufiyaa
        MWK, //	Malawi Kwacha
        MXN, //	Mexico Peso
        MYR, //	Malaysia Ringgit
        MZN, //	Mozambique Metical
        NAD, //	Namibia Dollar
        NGN, //	Nigeria Naira
        NIO, //	Nicaragua Cordoba
        NOK, //	Norway Krone
        NPR, //	Nepal Rupee
        NZD, //	New Zealand Dollar
        OMR, //	Oman Rial
        PAB, //	Panama Balboa
        PEN, //	Peru Nuevo Sol
        PGK, //	Papua New Guinea Kina
        PHP, //	Philippines Peso
        PKR, //	Pakistan Rupee
        PLN, //	Poland Zloty
        PYG, //	Paraguay Guarani
        QAR, //	Qatar Riyal
        RON, //	Romania New Leu
        RSD, //	Serbia Dinar
        RUB, //	Russia Ruble
        RWF, //	Rwanda Franc
        SAR, //	Saudi Arabia Riyal
        SBD, //	Solomon Islands Dollar
        SCR, //	Seychelles Rupee
        SDG, //	Sudan Pound
        SEK, //	Sweden Krona
        SGD, //	Singapore Dollar
        SHP, //	Saint Helena Pound
        SLL, //	Sierra Leone Leone
        SOS, //	Somalia Shilling
        SPL, //	Seborga Luigino
        SRD, //	Suriname Dollar
        STD, //	São Tomé and Príncipe Dobra
        SVC, //	El Salvador Colon
        SYP, //	Syria Pound
        SZL, //	Swaziland Lilangeni
        THB, //	Thailand Baht
        TJS, //	Tajikistan Somoni
        TMT, //	Turkmenistan Manat
        TND, //	Tunisia Dinar
        TOP, //	Tonga Pa'anga
        TRY, //	Turkey Lira
        TTD, //	Trinidad and Tobago Dollar
        TVD, //	Tuvalu Dollar
        TWD, //	Taiwan New Dollar
        TZS, //	Tanzania Shilling
        UAH, //	Ukraine Hryvnia
        UGX, //	Uganda Shilling
        UYU, //	Uruguay Peso
        UZS, //	Uzbekistan Som
        VEF, //	Venezuela Bolivar
        VND, //	Viet Nam Dong
        VUV, //	Vanuatu Vatu
        WST, //	Samoa Tala
        XAF, //	Communauté Financière Africaine (BEAC) CFA Franc BEAC
        XCD, //	East Caribbean Dollar
        XDR, //	International Monetary Fund (IMF) Special Drawing Rights
        XOF, //	Communauté Financière Africaine (BCEAO) Franc
        XPF, //	Comptoirs Français du Pacifique (CFP) Franc
        YER, //	Yemen Rial
        ZAR, //	South Africa Rand
        ZMW, //	Zambia Kwacha
        ZWD, //	Zimbabwe Dollar

        /* Cryptos
10-5,
2CH,
365
42
66
666
84
888

AAA

AC

ADN

ADT

AIM

AIR

ALC

ALF

ALN

AMC

AMK

ANC

ANI

ANT

AOC

APC

APE

APH

APP

ARG

ARK

ARS

ASC

ASCE

ASR

ATH

AUR

AUS

AXIS

BAC

BAT

BC

BCN

BCX

BDC

BEC

BEE

BEER

BEL

BELI

BEN

BET

BFC
 
BIL
 
BIRD
 
BITS
 
BLA
 
BLC
 
BLL
 
BLTZ
 
BLU
 
BLZ
 
BNS
 
BOB
 
BOC
 
BONES
 
BOS
 
BOY
 
BQC
 
BSC
 
BST
 
BTB
 
BTC
 
BTCs
 
BTE
 
BTG
 
BTL
 
BTN
 
BTP
 
BUK
 
BUN
 
BUR
 
C2
 
CACH
 
CAGE
 
CAP
 
CARB
 
CASH
 
CAT
 
CATc
 
CC
 
CCC
 
CCN
 
CCX
 
CDC
 
CDN
 
CENT
 
CGA
 
CGB
 
CHA
 
CHC
 
CHI
 
CIN
 
CINNI
 
CKC
 
CLOAK
 
CLR
 
CMC
 
CNC
 
CNOTE
 
COL
 
CON
 
CORG
 
COYE
 
CPR
 
CRC
 
CRD
 
CREA
 
CRN
 
CRY
 
CSC
 
CTC
 
CTD
 
CTM
 
CX
 
CYB
 
CYC
 
DBL
 
DDC
 
DEM
 
DGB
 
DGC
 
DIEM
 
DIG
 
DIME
 
DIS
 
DMD
 
DNC
 
DOGE
 
DOME
 
DOPE
 
DOT
 
DPS
 
DRK
 
DRM
 
DTC
 
DUCK
 
DVC
 
DVK
 
EAC
 
EBG
 
EBT
 
ECC
 
ECO
 
EDISON
 
EGC
 
ELC
 
ELIRA
 
ELP
 
EMC2
 
EMD
 
ENC
 
EON
 
ETC
 
EUC
 
EXC
 
EXE
 
EXN
 
EZC
 
FCK
 
FCN
 
FEC
 
FEC
 
FER
 
FFC
 
FIT
 
FLAP
 
FLC
 
FLO
 
FLT
 
FOX
 
FRAC
 
FRC
 
FRK
 
FRQ
 
FRX
 
FRY
 
FSS
 
FST
 
FTC
 
FUCK
 
FZ
 
GAC
 
GAY
 
GDC
 
GDN
 
GEO
 
GER
 
GFT
 
GHC
 
GIAR
 
GIL
 
GIRL
 
GIVE
 
GLB
 
GLC
 
GLD
 
GLM
 
GLN
 
GLT
 
GLX
 
GME
 
GNS
 
GOAT
 
GOOD
 
GOX
 
GPL
 
GPUC
 
GRA
 
GRC
 
GRCE
 
GRG
 
GRN
 
GRP
 
GRS
 
GRUM
 
GRW
 
GTC
 
GUN
 
H2O
 
H5C
 
HASH
 
HBN
 
HIRO
 
HKC
 
HOT
 
HPC
 
HTC
 
HTML
 
HUC
 
HVC
 
HXC
 
HYC
 
I0C
 
IC
 
IFC
 
IMP
 
INK
 
IPC
 
IRL
 
ISR
 
ITC
 
IXC
 
JKC
 
JKY
 
JNY
 
JPU
 
JUG
 
KARM
 
KDC
 
KDS
 
KED
 
KGC
 
KILR
 
KIWI
 
KKC
 
KRN
 
KRYP
 
KSC
 
LAT
 
LBW
 
LEAF
 
LGC
 
LGD
 
LIMX
 
LIRE
 
LK7
 
LKY
 
LMC
 
LOL
 
LOOT
 
LOT
 
LOVE
 
LPC
 
LTB
 
LTC
 
LTCX
 
LVC
 
LYC
 
MAC
 
MAGI
 
MAKI
 
MAMM
 
MARU
 
MAX
 
MBC
 
MEC
 
MED
 
MEG
 
MEM
 
MEOW
 
METH
 
MHA
 
MHYC
 
MINT
 
MLC
 
MMC
 
MNC
 
MNR
 
MOL
 
MONA
 
MOON
 
MPL
 
MRC
 
MRS
 
MRY
 
MST
 
MTC
 
MTS
 
MUN
 
MYC
 
MYM
 
MYR
 
MZC
 
NAN
 
NAUT
 
NBC
 
NBL
 
NDL
 
NEC
 
NET
 
NGT
 
NJA
 
NKA
 
NL
 
NLG
 
NMC
 
NOBL
 
NOTE
 
NRB
 
NUC
 
NUMC
 
NUT
 
NVC
 
NWC
 
NXT
 
NYAN
 
NYM
 
OFF
 
OGC
 
OLY
 
OMC
 
ONC
 
ONI
 
OPC
 
ORB
 
ORO
 
OSC
 
OZC
 
PAC
 
PAND
 
PAWN
 
PCN
 
PEC
 
PENG
 
PGC
 
PHC
 
PHI
 
PHS
 
PI
PIG
PIKA
PIR
PIX
PKR
PLN
PLT
PMC
PND
POK
POP
POT
PPC
PPL
PRC
PRIMIO
PRT
PT
PTC
PTS
PWC
PWNY
PXC
PXL
PYC
PYRA
Q2C
QBC
QBT
QCN
QQC
QRK
RAD
RAIN
RCH
RDD
REC
RED
RIC
RPC
RPD
RT2
RTC
RUBY
RUP
RYC
SAT
SAV
SBC
SBX
SC
SCO
SCOT
SDC
SFR
SHA
SHC
SHIBE
SIC
SKN
SLOTH
SLR
SMB
SMC
SNC
SOC
SOCHI
SOL
SPA
SPC
SPT
SRC
SRG
STA
STL
STR
STY
SUC
SUN
SUPER
SXC
SYN
SYNC
TAG
TAK
TBN
TBX
TCO
TEA
TEK
TES
TGC
THC
THOR
TIPS
TIX
TK
TOP
TPC
TRK
TTC
TTN
TWC
UFC
UFO
ULT
UNB
UNC
UNI
UNO
URO
USC
USDE
UTC
UVC
V
VAC
VC
VEL
VGC
VLC
VLT
VMP
VOLT
VRC
VTC
WAS
WC
WDC
WEC
WEST
WIKI
WIN
WKC
WPC
WUBS
X13C
XCO
XDC
XJO
XLB
XLC
XMR
XNC
XSS
XSV
XTN
XTP
XXC
XXL
YAC
YACC
YBC
YC
YMC
YUM
ZCC
ZED
ZEIT
ZET
ZEU
ZMB
ZS
ZTC
ZUR
kFC*/
    }
}
