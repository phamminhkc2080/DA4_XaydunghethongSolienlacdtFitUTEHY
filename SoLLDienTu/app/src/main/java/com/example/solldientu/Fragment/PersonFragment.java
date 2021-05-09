package com.example.solldientu.Fragment;

import android.content.Intent;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.solldientu.Api.ApiSinhVien;
import com.example.solldientu.LoginActivity;
import com.example.solldientu.R;
import com.example.solldientu.object.SinhVien;
import com.squareup.picasso.Picasso;

import de.hdodenhof.circleimageview.CircleImageView;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link PersonFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class PersonFragment extends Fragment {

    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    public PersonFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment PersonFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static PersonFragment newInstance(String param1, String param2) {
        PersonFragment fragment = new PersonFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);
        }
    }

    View view;
    TextView tv_MaSV, tv_TenSV, tv_MaLop;
    CircleImageView img_avt;
    String maSV;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        view = inflater.inflate(R.layout.fragment_person, container, false);
        Init();
        Intent it=getActivity().getIntent();
        maSV=it.getStringExtra("maSV");
        setData();
        Events();
        return view;
    }

    private void setData() {
        tv_MaSV.setText("Mã SV : " + maSV);
        ApiSinhVien.apiService.getById(maSV).enqueue(new Callback<SinhVien>() {
            @Override
            public void onResponse(Call<SinhVien> call, Response<SinhVien> response) {
                SinhVien sv=response.body();
                if (sv!=null){
                    tv_TenSV.setText(sv.getTenSv());
                    tv_MaLop.setText("Học lớp: "+ sv.getMaLop());
                    if (!sv.getAnh().equals("")){
                        String[] tenfile=sv.getAnh().split("\\.");
                        Picasso.get().load(ApiSinhVien.url+"GetImage/"+tenfile[0]).into(img_avt);
                    }else Picasso.get().load(R.drawable.ic_avt).into(img_avt);
                }
            }

            @Override
            public void onFailure(Call<SinhVien> call, Throwable t) {

            }
        });
    }

    private void Events() {
    }

    private void Init() {
        tv_MaSV = view.findViewById(R.id.MaSV_tv);
        tv_TenSV=view.findViewById(R.id.NameSV_tv);
        tv_MaLop=view.findViewById(R.id.maLop_tv);
        img_avt=view.findViewById(R.id.avt_img);
    }
}