package app.forms;

import app.entity.City;
import app.service.CityService;
import app.service.TrainService;
import app.util.HebirnateUtil;
import com.github.lgooddatepicker.components.DateTimePicker;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.time.LocalDateTime;
import java.util.List;

public class FormAddTrain extends JFrame {
    private JPanel panelMain;
    private JLabel labelStartCity;
    private JLabel labelFinishCity;
    private JLabel labelStartDateTime;
    private JLabel labelFinishDateTime;
    private JTextField fieldNumTickets;
    private JPanel panelFinishDateTime;
    private JPanel panelDateTime;
    private JButton buttonAddTrain;
    private JComboBox comboBoxFinishCity;
    private JComboBox comboBoxStartCity;
    private JLabel labelNumTickets;

    private DateTimePicker startDateTimePicker;
    private DateTimePicker finishDateTimePicker;
    List<City> cities;

    public FormAddTrain() {
        setTitle("Add Train");
        setContentPane(panelMain);
        setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
        setSize(800, 800);
        setVisible(true);
        panelMain.setLayout(new BoxLayout(panelMain, BoxLayout.Y_AXIS));

        setListCities();
        setDateTimePicker();

        buttonAddTrain.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                addTrain();
            }
        });
    }

    private void setListCities() {
        CityService cityService = new CityService(HebirnateUtil.getEntityManager());
        cities = cityService.GetAllCities();
        cities.forEach(city -> {
            comboBoxStartCity.addItem(city.getName());
            comboBoxFinishCity.addItem(city.getName());
        });
    }

    private void setDateTimePicker(){
        startDateTimePicker = new DateTimePicker();
        panelDateTime.add(startDateTimePicker);

        finishDateTimePicker = new DateTimePicker();
        panelFinishDateTime.add(finishDateTimePicker);
    }

    private void addTrain(){
        int startCityId = cities.stream()
                .filter(city -> city.getName() == comboBoxStartCity.getSelectedItem().toString())
                .mapToInt(City::getId)
                .findFirst()
                .orElse(-1);
        int finishCityId = cities.stream()
                .filter(city -> city.getName() == comboBoxFinishCity.getSelectedItem().toString())
                .mapToInt(City::getId)
                .findFirst()
                .orElse(-1);
        int numTickets = Integer.parseInt(fieldNumTickets.getText());
        LocalDateTime startDateTime = startDateTimePicker.getDateTimeStrict();
        LocalDateTime finishDateTime = finishDateTimePicker.getDateTimeStrict();
        TrainService trainService = new TrainService(HebirnateUtil.getEntityManager());
        boolean res = trainService.addTrain(startDateTime, finishDateTime, startCityId, finishCityId, numTickets);
        if (res){
            JOptionPane.showMessageDialog(this, "Train successfully added");
        } else {
            JOptionPane.showMessageDialog(this, "Train not added", "Error", JOptionPane.ERROR_MESSAGE);
        }
    }
}
