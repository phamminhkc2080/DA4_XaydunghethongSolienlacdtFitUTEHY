package com.example.solldientu.Fragment;

import android.content.Intent;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.ViewFlipper;

import com.example.solldientu.R;
import com.example.solldientu.SubjectsActivity;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link HomeFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class HomeFragment extends Fragment {

    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    public HomeFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment HomeFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static HomeFragment newInstance(String param1, String param2) {
        HomeFragment fragment = new HomeFragment();
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
    ViewFlipper viewFlipper;
    Animation in, out;
    TextView tv_notifi;
    LinearLayout llMonHoc;

    String maSV="";

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        view = inflater.inflate(R.layout.fragment_home, container, false);
        Init();
        maSV=getActivity().getIntent().getStringExtra("maSV");

        viewFlipper.setInAnimation(in);
        viewFlipper.setOutAnimation(out);
        viewFlipper.setFlipInterval(3000);
        viewFlipper.setAutoStart(true);
        Events();
        return view;
    }

    private void Events() {
        tv_notifi.setText("Trường Đại Học Sư Phạm Kỹ Thuật Hưng Yên - Khoa CNTT!");
        tv_notifi.setSelected(true);
        llMonHoc.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent it = new Intent(getActivity(), SubjectsActivity.class);
                it.putExtra("maSV", maSV);
                startActivity(it);
            }
        });

    }

    private void Init() {
        in = AnimationUtils.loadAnimation(getActivity(), R.anim.fade_in);
        out = AnimationUtils.loadAnimation(getActivity(), R.anim.fade_out);
        viewFlipper = view.findViewById(R.id.viewFliper);
        tv_notifi = view.findViewById(R.id.tv_notification);
        llMonHoc = view.findViewById(R.id.ln_bt1);
    }
}