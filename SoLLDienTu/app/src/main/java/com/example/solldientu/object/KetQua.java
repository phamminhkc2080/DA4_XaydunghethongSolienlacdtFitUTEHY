package com.example.solldientu.object;

public class KetQua {
    private String maMh, tenMh, soTc;
    int diemLd, diemTl;

    public KetQua(String maMh, String tenMh, String soTc, int diemLd, int diemTl) {
        this.maMh = maMh;
        this.tenMh = tenMh;
        this.soTc = soTc;
        this.diemLd = diemLd;
        this.diemTl = diemTl;
    }

    public String getMaMh() {
        return maMh;
    }

    public void setMaMh(String maMh) {
        this.maMh = maMh;
    }

    public String getTenMh() {
        return tenMh;
    }

    public void setTenMh(String tenMh) {
        this.tenMh = tenMh;
    }

    public String getSoTc() {
        return soTc;
    }

    public void setSoTc(String soTc) {
        this.soTc = soTc;
    }

    public int getDiemLd() {
        return diemLd;
    }

    public void setDiemLd(int diemLd) {
        this.diemLd = diemLd;
    }

    public int getDiemTl() {
        return diemTl;
    }

    public void setDiemTl(int diemTl) {
        this.diemTl = diemTl;
    }
}
