

.extern _memcopy    ; copy [w0] to [w1], by (w2+1) words
.extern _memswap    ; swap [w0],[w1], by (w2+1) words
.extern _memcpmac   ; w0: dest address
.extern _memcpip4
.extern _memswip4

.extern _memdump16
.extern _memdump64  ;64 bytes dump [w0] - [w0+63]

.extern _mBUFFER
