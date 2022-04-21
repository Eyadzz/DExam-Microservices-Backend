package handlers

import (
	"encoding/json"
	"exam_service/pkg/domain/exam"
	"exam_service/pkg/errs"
	"exam_service/pkg/service"
	"github.com/gin-gonic/gin"
	"net/http"
)

type ExamHandlers struct {
	service service.ExamService
}

func (examHandler ExamHandlers) Create(c *gin.Context) {
	c.Writer.Header().Add("Content-Type", "application/json")
	var newExam exam.Exam
	_ = json.NewDecoder(c.Request.Body).Decode(&newExam)
	err := examHandler.service.Create(newExam)

	//handling errors
	if err != nil && err.Error() == errs.ErrMarshallingInstance.Error() {
		c.Writer.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(c.Writer).Encode(errs.NewResponse(errs.ErrMarshallingInstance.Error(), http.StatusInternalServerError))
		return
	} else if err != nil && err.Error() == errs.ErrDb.Error() {
		c.Writer.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(c.Writer).Encode(errs.NewResponse(errs.ErrDb.Error(), http.StatusInternalServerError))
		return
	} else if err != nil && err.Error() == errs.ErrDuplicateExam.Error() {
		c.Writer.WriteHeader(http.StatusBadRequest)
		json.NewEncoder(c.Writer).Encode(errs.NewResponse(errs.ErrDuplicateExam.Error(), http.StatusBadRequest))
		return
	}

	//sending the response
	c.Writer.Header().Set("Access-Control-Allow-Origin", "*")
	c.Writer.WriteHeader(http.StatusOK)
	json.NewEncoder(c.Writer).Encode(newExam)
}

func (examHandler ExamHandlers) Read(c *gin.Context) {
	c.Writer.Header().Add("Content-Type", "application/json")
	allExams, err := examHandler.service.Read()
	if err != nil && err.Error() == errs.ErrDb.Error() {
		c.Writer.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(c.Writer).Encode(errs.NewResponse(errs.ErrDb.Error(), http.StatusInternalServerError))
		return
	} else if err != nil && err.Error() == errs.ErrUnmarshallingJson.Error() {
		c.Writer.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(c.Writer).Encode(errs.NewResponse(errs.ErrUnmarshallingJson.Error(), http.StatusInternalServerError))
		return
	}
	//sending the response
	c.Writer.WriteHeader(http.StatusOK)
	json.NewEncoder(c.Writer).Encode(allExams)
}
