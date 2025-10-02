# PC PARTS

# Motherboard

# PSU

# CPU
Intel: 
i3-14100 - $109
CST: 3750
CMT: 15200

i5-14600K - $179
CST: 4250
CMT: 38700

i7-14700K - $299
CST: 4450
CMT: 52300

i9-14900K - $419
CST: 4700
CMT: 58600

Ultra 5 245K - $239
CST: 4700
CMT: 43450

Ultra 7 265K - $289
CST: 4900
CMT: 58800

Ultra 9 285K - $519
CST: 5100
CMT: 67650

Xeon Gold 6312U - $1699
CST: 2350
CMT: 42450

Xeon Gold 6534 - $3399
CST: 3450 
CMT: 29500

Xeon W9-3495X - $6299
CST: 3500
CMT: 90700

AMD:
Ryzen 5 9600X - $179
CST: 4550
CMT: 30000

Ryzen 7 9700X - $299
CST: 4650
CMT: 37150

Ryzen 7 9800X3D - $459
CST: 4450
CMT: 40000

Ryzen 9 9900X3D - $499
CST: 4650
CMT: 56200

Ryzen 9 9950X3D - $649
CST: 4750
CMT: 70200

Threadripper 9980X - $4999
CST: 4500
CMT: 145700

Threadripper PRO 9955WX - $1649
CST: 4450
CMT: 68350

Threadripper PRO 9985WX - $7999
CST: 4500
CMT: 154850

Threadripper PRO 9995WX - $11699
CST: 4600
CMT: 175750


# GPU

NVIDIA:
RTX PRO 6000 - $8999
GPU: 42200

RTX 5090 - $2299
GPU: 39150

RTX 5080 - $999
GPU: 36200

RTX 5070ti - $749
GPU: 32800

RTX 5070 - $479
GPU: 29000

RTX 5060ti - $369
GPU: 22800

RTX 5060 - $299
GPU: 20800

RTX 5050 - $249
GPU: 16900


AMD: 
RX 9070XT - $669
GPU: 26900

RX 9070 - $599
GPU: 25350

RX 9060XT - $279
GPU: 20000


Intel: 
Arc B580 - $249
GPU: 15900

Arc B570 - $199
GPU: 14200


# RAM

# Storage

# Cooler

# Case


# Prebuilts
Mac Mini M4 10c/10g 16gb - $599
CST: 4550
CMT: 23900
GPU:
URAM: 120gb/s

Mac Mini M4 Pro 12c/16g 24gb - $1399
CST: 4600
CMT: 32400
GPU:
URAM: 273gb/s

Mac Studio M4 Max 14c/32g 36gb - $1999
CST: 4600
CMT: 38500
GPU:
URAM: 410gb/s

Mac Studio M3 Ultra 28c/60g 96gb - $3999
CST: 5100
CMT: 68750
GPU:
URAM: 819gb/s



# USE CASES AND WEIGHTING

Web browsing, text docs, emails: 
CST: Linear
CMT: Logarithmic
GPU: Irrelevant
SRAM: Sqrt up to 8, then Cbrt
VRAM: Irrelevant

Software compiling:
CST: Linear
CMT: Linear 
GPU: Irrelevant
SRAM: Linear up to 32, then Sqrt
VRAM: Irrelevant

3d modeling/rendering:
CST: Linear
CMT: Linear
GPU: Linear
SRAM: Linear up to 32, then Logarithmic
VRAM: Linear

Photo editing:
CST: Linear
CMT: Logarithmic
GPU: Linear
SRAM: Linear up to 32, then Cbrt
VRAM: Linear up to 6, then Cbrt

Video editing:
CST: Linear
CMT: Linear
GPU: Linear
SRAM: Logarithmic up to 64, then Sqrt
VRAM: Linear up to 16, then Logarithmic

AI/ML:
CST: Linear
CMT: Linear
GPU: Linear
SRAM: Linear up to 128, then Logarithmic
VRAM: Binary, model needs to fit in VRAM
(scored by % of models that can fit)

Gaming
CST: Linear + Logarithmic
CMT: Logarithmic
GPU: Linear
SRAM: Linear up to 32, then Sqrt
VRAM: Linear up to 12, then Logarithmic

Music production
CST: Linear
CMT: Linear
GPU: Irrelevant
SRAM: Linear up to 64, then Sqrt
VRAM: Irrelevant

Physics/Scientific simulations
CST: Linear
CMT: Linear
GPU: Linear
SRAM: Linear to 128, then Logarithmic
VRAM: Linear to 24, then Logarithmic




