from google.protobuf import empty_pb2 as _empty_pb2
from google.protobuf.internal import containers as _containers
from google.protobuf.internal import enum_type_wrapper as _enum_type_wrapper
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from collections.abc import Iterable as _Iterable, Mapping as _Mapping
from typing import ClassVar as _ClassVar, Optional as _Optional, Union as _Union

DESCRIPTOR: _descriptor.FileDescriptor

class AlgorithmType(int, metaclass=_enum_type_wrapper.EnumTypeWrapper):
    __slots__ = ()
    DQN: _ClassVar[AlgorithmType]
    PPO: _ClassVar[AlgorithmType]
    A2C: _ClassVar[AlgorithmType]
    DDPG: _ClassVar[AlgorithmType]
    SAC: _ClassVar[AlgorithmType]
    TD3: _ClassVar[AlgorithmType]
DQN: AlgorithmType
PPO: AlgorithmType
A2C: AlgorithmType
DDPG: AlgorithmType
SAC: AlgorithmType
TD3: AlgorithmType

class EnvMessage(_message.Message):
    __slots__ = ("reset_request", "reset_response", "step_request", "step_response", "training_end_request", "training_end_response")
    RESET_REQUEST_FIELD_NUMBER: _ClassVar[int]
    RESET_RESPONSE_FIELD_NUMBER: _ClassVar[int]
    STEP_REQUEST_FIELD_NUMBER: _ClassVar[int]
    STEP_RESPONSE_FIELD_NUMBER: _ClassVar[int]
    TRAINING_END_REQUEST_FIELD_NUMBER: _ClassVar[int]
    TRAINING_END_RESPONSE_FIELD_NUMBER: _ClassVar[int]
    reset_request: ResetRequest
    reset_response: ResetResponse
    step_request: Step
    step_response: StepResult
    training_end_request: TrainingEnd
    training_end_response: TrainingEnd
    def __init__(self, reset_request: _Optional[_Union[ResetRequest, _Mapping]] = ..., reset_response: _Optional[_Union[ResetResponse, _Mapping]] = ..., step_request: _Optional[_Union[Step, _Mapping]] = ..., step_response: _Optional[_Union[StepResult, _Mapping]] = ..., training_end_request: _Optional[_Union[TrainingEnd, _Mapping]] = ..., training_end_response: _Optional[_Union[TrainingEnd, _Mapping]] = ...) -> None: ...

class ResetRequest(_message.Message):
    __slots__ = ("env_id",)
    ENV_ID_FIELD_NUMBER: _ClassVar[int]
    env_id: str
    def __init__(self, env_id: _Optional[str] = ...) -> None: ...

class TrainingEnd(_message.Message):
    __slots__ = ("is_end",)
    IS_END_FIELD_NUMBER: _ClassVar[int]
    is_end: bool
    def __init__(self, is_end: bool = ...) -> None: ...

class ModelPredictActionRequest(_message.Message):
    __slots__ = ("observation",)
    OBSERVATION_FIELD_NUMBER: _ClassVar[int]
    observation: _containers.RepeatedScalarFieldContainer[int]
    def __init__(self, observation: _Optional[_Iterable[int]] = ...) -> None: ...

class ResetResponse(_message.Message):
    __slots__ = ("observation",)
    OBSERVATION_FIELD_NUMBER: _ClassVar[int]
    observation: _containers.RepeatedScalarFieldContainer[int]
    def __init__(self, observation: _Optional[_Iterable[int]] = ...) -> None: ...

class TrainingEnvRequest(_message.Message):
    __slots__ = ("env_id", "algorithm", "policy", "learningRate", "gamma", "totalTimeSteps", "device")
    ENV_ID_FIELD_NUMBER: _ClassVar[int]
    ALGORITHM_FIELD_NUMBER: _ClassVar[int]
    POLICY_FIELD_NUMBER: _ClassVar[int]
    LEARNINGRATE_FIELD_NUMBER: _ClassVar[int]
    GAMMA_FIELD_NUMBER: _ClassVar[int]
    TOTALTIMESTEPS_FIELD_NUMBER: _ClassVar[int]
    DEVICE_FIELD_NUMBER: _ClassVar[int]
    env_id: str
    algorithm: AlgorithmType
    policy: str
    learningRate: float
    gamma: float
    totalTimeSteps: int
    device: str
    def __init__(self, env_id: _Optional[str] = ..., algorithm: _Optional[_Union[AlgorithmType, str]] = ..., policy: _Optional[str] = ..., learningRate: _Optional[float] = ..., gamma: _Optional[float] = ..., totalTimeSteps: _Optional[int] = ..., device: _Optional[str] = ...) -> None: ...

class Step(_message.Message):
    __slots__ = ("action",)
    ACTION_FIELD_NUMBER: _ClassVar[int]
    action: _containers.RepeatedScalarFieldContainer[int]
    def __init__(self, action: _Optional[_Iterable[int]] = ...) -> None: ...

class StepResult(_message.Message):
    __slots__ = ("observation", "reward", "terminated", "truncated", "info")
    class InfoEntry(_message.Message):
        __slots__ = ("key", "value")
        KEY_FIELD_NUMBER: _ClassVar[int]
        VALUE_FIELD_NUMBER: _ClassVar[int]
        key: str
        value: str
        def __init__(self, key: _Optional[str] = ..., value: _Optional[str] = ...) -> None: ...
    OBSERVATION_FIELD_NUMBER: _ClassVar[int]
    REWARD_FIELD_NUMBER: _ClassVar[int]
    TERMINATED_FIELD_NUMBER: _ClassVar[int]
    TRUNCATED_FIELD_NUMBER: _ClassVar[int]
    INFO_FIELD_NUMBER: _ClassVar[int]
    observation: _containers.RepeatedScalarFieldContainer[int]
    reward: float
    terminated: bool
    truncated: bool
    info: _containers.ScalarMap[str, str]
    def __init__(self, observation: _Optional[_Iterable[int]] = ..., reward: _Optional[float] = ..., terminated: bool = ..., truncated: bool = ..., info: _Optional[_Mapping[str, str]] = ...) -> None: ...

class SendStepContinuousActionNotify(_message.Message):
    __slots__ = ("action",)
    ACTION_FIELD_NUMBER: _ClassVar[int]
    action: _containers.RepeatedScalarFieldContainer[float]
    def __init__(self, action: _Optional[_Iterable[float]] = ...) -> None: ...

class StepContinuousResultResponse(_message.Message):
    __slots__ = ("observation", "reward", "terminated", "truncated", "info")
    class InfoEntry(_message.Message):
        __slots__ = ("key", "value")
        KEY_FIELD_NUMBER: _ClassVar[int]
        VALUE_FIELD_NUMBER: _ClassVar[int]
        key: str
        value: str
        def __init__(self, key: _Optional[str] = ..., value: _Optional[str] = ...) -> None: ...
    OBSERVATION_FIELD_NUMBER: _ClassVar[int]
    REWARD_FIELD_NUMBER: _ClassVar[int]
    TERMINATED_FIELD_NUMBER: _ClassVar[int]
    TRUNCATED_FIELD_NUMBER: _ClassVar[int]
    INFO_FIELD_NUMBER: _ClassVar[int]
    observation: _containers.RepeatedScalarFieldContainer[float]
    reward: float
    terminated: bool
    truncated: bool
    info: _containers.ScalarMap[str, str]
    def __init__(self, observation: _Optional[_Iterable[float]] = ..., reward: _Optional[float] = ..., terminated: bool = ..., truncated: bool = ..., info: _Optional[_Mapping[str, str]] = ...) -> None: ...
