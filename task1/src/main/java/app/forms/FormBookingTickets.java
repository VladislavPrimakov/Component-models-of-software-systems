package app.forms;

import app.entity.City;
import app.entity.Train;
import app.entity.User;
import app.service.CityService;
import app.service.TicketService;
import app.service.TrainService;
import app.util.HebirnateUtil;
import com.github.lgooddatepicker.components.DatePicker;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.List;

public class FormBookingTickets extends JFrame {
    private JPanel panelMain;
    private JComboBox comboBoxStartCity;
    private JLabel labelStartCity;
    private JComboBox comboBoxFinishCity;
    private JLabel labelFinishCity;
    private JLabel labelStartDate;
    private JPanel panelFinishDate;
    private JPanel panelDate;
    private JButton buttonSearch;
    private JPanel panelSearch;
    private JPanel panelSearchOther;
    private JLabel labelTrainsBefore;

    private DatePicker datePicker;
    List<City> cities;
    User user;

    public FormBookingTickets(User user) {
        this.user = user;

        setTitle("Booking tickets");
        setContentPane(panelMain);
        setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
        setSize(800, 800);
        setVisible(true);

        panelSearch.setLayout(new BoxLayout(panelSearch, BoxLayout.Y_AXIS));
        panelSearchOther.setLayout(new BoxLayout(panelSearchOther, BoxLayout.Y_AXIS));

        buttonSearch.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                search();
            }
        });

        setListCities();
        setDatePicker();
    }

    private void setListCities() {
        CityService cityService = new CityService(HebirnateUtil.getEntityManager());
        cities = cityService.GetAllCities();
        cities.forEach(city -> {
            comboBoxStartCity.addItem(city.getName());
            comboBoxFinishCity.addItem(city.getName());
        });
    }

    private void setDatePicker() {
        datePicker = new DatePicker();
        panelDate.add(datePicker);
    }

    private void search() {
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
        LocalDate date = datePicker.getDate();
        TrainService trainService = new TrainService(HebirnateUtil.getEntityManager());
        List<Train> trains = trainService.getAllTrains(date, startCityId, finishCityId);
        panelSearch.removeAll();
        trains.forEach(train -> setTrain(train, panelSearch));
        panelSearch.revalidate();
        panelSearch.repaint();

        panelSearchOther.removeAll();
        if (date != null){
            List<Train> trainsBefore = trainService.getAllTrains(LocalDateTime.now(), date.atStartOfDay(), startCityId, finishCityId);
            trainsBefore.forEach(train -> setTrain(train, panelSearchOther));

        }
        panelSearchOther.revalidate();
        panelSearchOther.repaint();
    }

    private void setTrain(Train train, JPanel panelMain) {
        JPanel panel = new JPanel(new FlowLayout(FlowLayout.LEFT, 5, 5));
        panel.add(new Label(train.getStartCity().getName() + " -> " + train.getFinishCity().getName()));
        panel.add(new Label(train.getStartTime().toString()));
        panel.add(new Label(train.getFinishTime().toString()));
        train.getTickets().stream().forEach(ticket -> {
            if (ticket.getStatus() == false) {
                JButton btn = new JButton("Book ticket " + ticket.getNumber());
                panel.add(btn);
                btn.addActionListener(new ActionListener() {
                    @Override
                    public void actionPerformed(ActionEvent e) {
                        book(ticket.getId());
                    }
                });
            }
        });
        panelMain.add(panel);
    }

    private void book(int ticketId){
        TicketService ticketService = new TicketService(HebirnateUtil.getEntityManager());
        boolean res = ticketService.setUserToTicket(ticketId, user.getId());
        if (res) {
            JOptionPane.showMessageDialog(this, "Ticket Booked");
        } else {
            JOptionPane.showMessageDialog(this, "Ticket Not Booked");
        }
        search();
    }

}
