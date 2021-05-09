package com.example.solldientu;

import android.os.Bundle;
import android.view.MenuItem;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.FragmentTransaction;

import com.example.solldientu.Fragment.HomeFragment;
import com.example.solldientu.Fragment.PersonFragment;
import com.example.solldientu.Fragment.ResultFragment;
import com.google.android.material.bottomnavigation.BottomNavigationView;

public class MainActivity extends AppCompatActivity {

    BottomNavigationView navigationView;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Init();
        Events();
    }

    private void Events() {
        HomeFragment fragment = new HomeFragment();
        FragmentTransaction ft = getSupportFragmentManager().beginTransaction();
        ft.replace(R.id.content, fragment, "");
        ft.commit();

        navigationView.setOnNavigationItemSelectedListener(new BottomNavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                switch (item.getItemId()) {
                    case R.id.nav_home:
                        HomeFragment homeFragment = new HomeFragment();
                        FragmentTransaction ft1 = getSupportFragmentManager().beginTransaction();
                        ft1.replace(R.id.content, homeFragment, "");
                        ft1.commit();
                        return true;
                    case R.id.nav_result:
                        ResultFragment resultFragment = new ResultFragment();
                        FragmentTransaction ft2 = getSupportFragmentManager().beginTransaction();
                        ft2.replace(R.id.content, resultFragment, "");
                        ft2.commit();
                        return true;
                    case R.id.nav_person:
                        PersonFragment personFragment = new PersonFragment();
                        FragmentTransaction ft3 = getSupportFragmentManager().beginTransaction();
                        ft3.replace(R.id.content, personFragment, "");
                        ft3.commit();
                        return true;
                }
                return false;
            }
        });
    }

    private void Init() {
        navigationView = findViewById(R.id.nav);
    }
}
