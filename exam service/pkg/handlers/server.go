package handlers

import (
	"exam_service/pkg/domain/repositories"
	"exam_service/pkg/driver"
	"exam_service/pkg/service"
	"github.com/gin-gonic/gin"
	"github.com/go-playground/validator/v10"
	"github.com/spf13/viper"
)

var validate *validator.Validate

func Start() {

	validate = validator.New()

	router := gin.Default()
	redisDb, redisJsonDb := driver.GetDbConnection()

	examHandler := ExamHandlers{service.NewExamService(repositories.NewExamRepositoryDb(redisDb, redisJsonDb))}

	router.POST("/api/exam/create-exam", examHandler.Create)
	router.GET("/api/exam/get-all-exams/:courseId", examHandler.GetCourseExams)
	router.GET("/api/exam/get-exam/:examId", examHandler.GetExam)
	router.DELETE("/api/exam/delete-exam/:examId", examHandler.DelExam)
	router.PUT("/api/exam/update-exam-info/:examId", examHandler.UpdateExamInfo)

	router.Run(viper.GetString("SERVER_PORT"))

}
