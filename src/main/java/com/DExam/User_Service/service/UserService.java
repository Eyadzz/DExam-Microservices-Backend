package com.DExam.User_Service.service;

import com.DExam.User_Service.database.UserRepository;
import com.DExam.User_Service.exception.*;
import com.DExam.User_Service.domain.User;
import com.DExam.User_Service.model.CourseStudentsInfo;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.ArrayList;

@Service
@RequiredArgsConstructor
public class UserService implements UserDetailsService, IUserService {

    private final UserRepository userRepository;
    private final BCryptPasswordEncoder bCryptPasswordEncoder;


    public User get(String email) {
        return userRepository.findByEmail(email).orElseThrow(UserNotFoundException::new);
    }

    public long save(User newUser) {
        newUser.setPassword(bCryptPasswordEncoder.encode(newUser.getPassword()));
        userRepository.save(newUser);
        return newUser.getId();
    }

    public void userExistByEmail(String email){
        if (userRepository.findByEmail(email).isPresent()) {
            throw new EmailExistException();
        }
    }

    public void userExistByNationalID(String nationalID){
         if (userRepository.findByNationalID(nationalID).isPresent()) {
             throw new NationalIDException();
         }
    }

    @Override
    public UserDetails loadUserByUsername(String email) throws UsernameNotFoundException {
        User user = userRepository.findByEmail(email).orElseThrow(UserNotFoundException::new);

        return new org.springframework.security.core.userdetails.User(user.getEmail(),user.getPassword(),new ArrayList<>());
    }

    public void updatePassword(String email, String oldPassword, String newPassword) {
        User user = userRepository.findByEmail(email).orElseThrow(UserNotFoundException::new);
        if (bCryptPasswordEncoder.matches(oldPassword, user.getPassword())) {
            user.setPassword(bCryptPasswordEncoder.encode(newPassword));
            userRepository.save(user);
        } else {
            throw new IncorrectPasswordException();
        }
    }

    @Override
    public boolean isUserActive(String email) {
        return userRepository.findByEmail(email).orElseThrow(EmailNotExistException::new).isActive();
    }

    @Override
    public ArrayList<CourseStudentsInfo> getUsers(ArrayList<Long> userIDs) {
        ArrayList<CourseStudentsInfo> courseStudentsInfos = new ArrayList<>();
        for (Long userID : userIDs) {
            User user = userRepository.findById(userID).orElseThrow(UserNotFoundException::new);
            courseStudentsInfos.add(new CourseStudentsInfo(user.getId(), user.getName(), user.getImg()));
        }
        return courseStudentsInfos;
    }

    public long activateUser(String email, String password) {
        User user = userRepository.findByEmail(email).orElseThrow(EmailNotExistException::new);
        user.setActive(true);
        user.setPassword(bCryptPasswordEncoder.encode(password));
        userRepository.save(user);
        return user.getId();
    }
}
