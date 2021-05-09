package com.example.solldientu.Adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.solldientu.R;
import com.example.solldientu.object.KetQua;
import com.example.solldientu.object.MonHoc;

import java.util.ArrayList;

public class ResultAdapter extends BaseAdapter {
    Context context;
    int layout;
    ArrayList<KetQua> dskq;

    public ResultAdapter(Context context, int layout, ArrayList<KetQua> dskq) {
        this.context = context;
        this.layout = layout;
        this.dskq = dskq;
    }

    @Override
    public int getCount() {
        return dskq.size();
    }

    @Override
    public Object getItem(int i) {
        return null;
    }

    @Override
    public long getItemId(int i) {
        return 0;
    }

    class ViewHolder {
        TextView tv_TenMon, tv_soTC, tv_DiemLD, tv_DiemTL;
    }
    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {
        ViewHolder holder;
        if (view == null) {
            LayoutInflater inflater = (LayoutInflater) context.getSystemService(context.LAYOUT_INFLATER_SERVICE);
            view = inflater.inflate(layout, null);
            holder = new ViewHolder();
            //
            holder.tv_TenMon = view.findViewById(R.id.tv_TenMon);
            holder.tv_soTC = view.findViewById(R.id.tv_soTC);
            holder.tv_DiemLD = view.findViewById(R.id.tv_DiemLD);
            holder.tv_DiemTL = view.findViewById(R.id.tv_DiemTL);
            view.setTag(holder);
        } else {
            holder = (ViewHolder) view.getTag();
        }

        KetQua mh = dskq.get(i);
        if (mh.getDiemTl()>0){
            holder.tv_TenMon.setText(mh.getTenMh());
            holder.tv_soTC.setText("Số tín chỉ: " + mh.getSoTc());
            holder.tv_DiemLD.setText("Điểm LĐ: " + mh.getDiemLd());
            holder.tv_DiemTL.setVisibility(View.VISIBLE);
            holder.tv_DiemTL.setText("Điểm TL: "+mh.getDiemTl());
        }else {
            holder.tv_TenMon.setText(mh.getTenMh());
            holder.tv_soTC.setText("Số tín chỉ: " + mh.getSoTc());
            holder.tv_DiemLD.setText("Điểm: " + mh.getDiemLd());

            holder.tv_DiemTL.setVisibility(View.GONE);
        }
        return view;
    }
}
